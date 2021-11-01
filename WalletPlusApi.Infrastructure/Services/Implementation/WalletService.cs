using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletPlusApi.Core.Common.Responses;
using WalletPlusApi.Core.Constants;
using WalletPlusApi.Core.Data;
using WalletPlusApi.Core.DTOs;
using WalletPlusApi.Core.Util;
using WalletPlusApi.Infrastructure.Persistence;
using WalletPlusApi.Infrastructure.Services.Interfaces;
using WalletPlusApi.Infrastructure.Services.Util;

namespace WalletPlusApi.Infrastructure.Services.Implementation
{
    public class WalletService : IWalletService
    {
        #region Fields
        private readonly IRepository<PointWallet> _ptwalletrepo;
        private readonly IRepository<Transaction> _transrepo;

        private readonly IRepository<Customer> _cstrepo;
        private readonly IMock3RDPartyService _mock3RDPartyService;
        private readonly IBillerService _biller;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CustomerService> _log;

        #endregion
        public WalletService(IRepository<PointWallet> ptwalletrepo, IRepository<Customer> cstrepo, IMock3RDPartyService mock3RDPartyService = null, IRepository<Transaction> transrepo = null, IHttpContextAccessor httpContextAccessor = null, ILogger<CustomerService> log = null, IBillerService biller = null)
        {
            _ptwalletrepo = ptwalletrepo;
            _cstrepo = cstrepo;
            _mock3RDPartyService = mock3RDPartyService;
            _transrepo = transrepo;
            _httpContextAccessor = httpContextAccessor;
            _log = log;
            _biller = biller;
        }
        /// <summary>
        /// Customer adding money to wallet
        /// </summary>
        /// <param name="reqModel"></param>
        /// <returns></returns>
        public async Task<BaseResponse> AddMoney(AddMoneyDTO reqModel)
        {
            var depositingCst = await ServiceUtil.GetCustomerDetails(_httpContextAccessor.HttpContext, _cstrepo);
            if (depositingCst == null) return Response.BadRequest(null, $"Customer doesnt exist, please open a wallet.");
            
            var pointCalc = (int)UtilMethods.CalculatePointsEarned(reqModel.AmountToAdd);
            var resp = new AddMoneyResponseDTO();
            
            //If customer wants to add money to self use 3rdPartyService
            if (!string.IsNullOrEmpty(reqModel.DepositingWalletId))
            {
                var reqTo3RDParty = _mock3RDPartyService.CompleteAddMoney(reqModel);
                if (!reqTo3RDParty.Status) return Response.BadRequest(reqTo3RDParty);
                var cstWallet1 = await _ptwalletrepo.Get(t => t.WalletId == reqModel.DepositingWalletId);
                if (cstWallet1 == null) return Response.BadRequest(null);
                
                cstWallet1.BalanceAmount += reqModel.AmountToAdd;
                //calculate point earned
                cstWallet1.PointEarned += pointCalc;
                await _ptwalletrepo.Save();
                //post to transactions table
                var createTran = new Transaction
                {
                    CreatedAt = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    TransRef = $"WPlus_{UtilMethods.GenerateRandomString(6)}",
                    BeneficiaryId = cstWallet1.CustomerId,
                    AmountAdded = reqModel.AmountToAdd,
                    Status = ResponseStatus.Success.Message,
                    DepositorId = depositingCst.Id,
                    CustomerId = depositingCst.Id
                };

                try
                {
                    await _transrepo.Add(createTran);
                    await _transrepo.Save();
                    _log.LogInformation($"transaction created succesfully.");
                }
                catch (Exception e)
                {
                    _log.LogError($"transaction created unsuccesfully.{e.Message}");
                    return Response.BadRequest(null);

                }
                resp.AmountToAdd = reqModel.AmountToAdd;
                resp.DepositingWalletId = reqModel.DepositingWalletId;
                resp.Message = $"{reqModel.AmountToAdd} added to Wallet Id {reqModel.DepositingWalletId}.";
                resp.TopUpMode = TopUpMode.ThirdParty.ToString();
                return Response.Ok(resp);
            }
            
            return Response.BadRequest(null, $"Something went wrong.");

        }

        public async Task<(PointWallet createdWallet, string message)> CreateWallet(CustomerDTO reqModel)
        {
            var CheckCustomer = await _cstrepo.Get(t => t.Email == reqModel.Email);
            if(CheckCustomer == null)
            {
                return (createdWallet: null, message: $"Something went wrong.");
            }
            var newWallet = new PointWallet
            {
                CreatedAt = DateTime.Now,
                LastUpdated = DateTime.Now,
                CustomerId = CheckCustomer.Id,
                WalletId = UtilMethods.GenerateRandomString(NumericConstants.StringLength),
                BalanceAmount=0.00m,
                PointEarned=0,
                MoneyValueForPointEarned =0.00m,
                CurrencySymbol = "USD"
            };
            try
            {
                await _ptwalletrepo.Add(newWallet);
                var (id, IsSaved) = await _ptwalletrepo.Save();
                //_log.LogInformation($"{id}");
                if (IsSaved)
                {
                    return (newWallet, $"Wallet created {newWallet.WalletId}.");
                }

            }catch(Exception e)
            {
                _log.LogError(e.Message);
                return (null, $"Wallet created Unsuccessfully. {e.Message}");
            }
            return (null, $"Wallet created Unsuccessfully.");

        }

        public async Task<BaseResponse> GetWalletBalances(bool GetMoneyBalance, bool GetPointBalance)
        {
            var depositingCst = await ServiceUtil.GetCustomerDetails(_httpContextAccessor.HttpContext, _cstrepo);
            if (depositingCst == null) return Response.BadRequest($"Customer doesnt exist, please open a wallet.");

            if(GetMoneyBalance && GetPointBalance)
            {
                var getBal = await _ptwalletrepo.Get(t => t.CustomerId == depositingCst.Id);
                if (getBal == null) return Response.BadRequest($"Customer Wallet doesnt exist, please open a wallet.");

                var walletBalanceResp = new WalletBalanceDTO
                {
                    Balance = getBal.BalanceAmount,
                    Email = depositingCst.Email,
                    FirstName = depositingCst.FirstName,
                    PointEarned = getBal.PointEarned,
                    WalletId = getBal.WalletId
                };
                return Response.Ok(walletBalanceResp, $"Wallet balances returned.");
            }
            return Response.BadRequest(null, $"Wallet balances retrieval failed.");


        }

        public async Task<BaseResponse> GetWalletTransactions(int skip, int take)
        {
            var depositingCst = await ServiceUtil.GetCustomerDetails(_httpContextAccessor.HttpContext, _cstrepo);
            if (depositingCst == null) return Response.BadRequest($"Customer doesnt exist, please open a wallet.");

            var getwallettrans = await _transrepo.GetAllPaginatedUsingSkipTakeWithOrderby(skip, take, t => t.CustomerId == depositingCst.Id);
            if (getwallettrans == null) return Response.OkObj(getwallettrans, 0, skip, take, 0, $"NO records retrieved.");
            var totaltrans = _transrepo.TotalCount(t => t.CustomerId == depositingCst.Id);
            return Response.OkObj(new TransactionResponseDTO().CreateTxnMap(getwallettrans), await totaltrans, skip, take, getwallettrans.Count, $"Returned {getwallettrans.Count} records.");
        }

        public async Task<BaseResponse> SendMoney(SendMoneyDTO reqModel)
        {
            var depositingCst = await ServiceUtil.GetCustomerDetails(_httpContextAccessor.HttpContext, _cstrepo);
            if (depositingCst == null) return Response.BadRequest(null, $"Customer doesnt exist, please open a wallet.");

            var pointCalc = (int)UtilMethods.CalculatePointsEarned(reqModel.AmountToAdd);
            var resp = new AddMoneyResponseDTO();
            var saveFlag = false;
            //if its to a walletid holder then debit directly,
            //before that validate the beneficiary walletid
            var GetBeneficiaryWallet = await _ptwalletrepo.Get(t => t.WalletId == reqModel.BeneficiaryWalletId);
            if (GetBeneficiaryWallet == null) return Response.BadRequest(null, $"Enter valid beneficiary wallet ID.");

            var cstWallet = await _ptwalletrepo.Get(t => t.WalletId == reqModel.DepositingWalletId);
            if (cstWallet == null) return Response.BadRequest(null);

            //Check mode of transfr 3rd party or wallet
            if (reqModel.TopUpMode == (int)TopUpMode.ThirdParty)
            {
                // 3rd party debits whatever mode of payment used here.
                var reqTo3RDParty = _mock3RDPartyService.CompleteSendMoney(reqModel);
                if (!reqTo3RDParty.Status) return Response.BadRequest(reqTo3RDParty);

                GetBeneficiaryWallet.BalanceAmount += reqModel.AmountToAdd;
                // since money is been added also calcalute point for beneficiary
                GetBeneficiaryWallet.PointEarned += pointCalc;
                saveFlag = true;
            }
            if (reqModel.TopUpMode == (int)TopUpMode.CustomerWallet)
            {
                // check depositor wallet if balance is enough
                if (cstWallet.BalanceAmount >= reqModel.AmountToAdd)
                {
                    //credit beneficiary
                    GetBeneficiaryWallet.BalanceAmount += reqModel.AmountToAdd;

                    // since money is been added also calcalute point for beneficiary
                    GetBeneficiaryWallet.PointEarned += pointCalc;

                    //debit depositor
                    cstWallet.BalanceAmount -= reqModel.AmountToAdd;
                    saveFlag = true;
                }
                else
                {
                    return Response.BadRequest(null, $"Insufficient funds.");
                }
            }

            if (saveFlag)
            {
                await _ptwalletrepo.Save();

                var createTrans = new Transaction
                {
                    CreatedAt = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    TransRef = $"WPlus_{UtilMethods.GenerateRandomString(6)}",
                    BeneficiaryId = GetBeneficiaryWallet.CustomerId,
                    AmountAdded = reqModel.AmountToAdd,
                    TopUpMode=reqModel.TopUpMode == (int)TopUpMode.ThirdParty ?
                                TopUpMode.ThirdParty.ToString():
                                TopUpMode.CustomerWallet.ToString(),
                    Status = ResponseStatus.Success.Message,
                    DepositorId = depositingCst.Id,
                    CustomerId = depositingCst.Id,
                    IsWithdrawal=true
                };
                try
                {
                    await _transrepo.Add(createTrans);
                    var (id, IsSaved) = await _transrepo.Save();
                    _log.LogInformation($"transaction created succesfully.");
                }
                catch (Exception e)
                {
                    _log.LogError($"transaction created unsuccesfully. {e.Message}");
                }

                resp.AmountToAdd = reqModel.AmountToAdd;
                resp.DepositingWalletId = reqModel.DepositingWalletId;
                resp.BeneficiaryWalletId = reqModel.BeneficiaryWalletId;
                resp.Message = $"{reqModel.AmountToAdd} added.";
                resp.TopUpMode = reqModel.TopUpMode == (int)TopUpMode.CustomerWallet ?
                                                            TopUpMode.CustomerWallet.ToString() :
                                                            TopUpMode.ThirdParty.ToString();

                return Response.Ok(resp);
            }
            return Response.BadRequest(null);
        }

        public async Task<BaseResponse> SpendMoney(SpendMoneyDTO reqModel)
        {
            var depositingCst = await ServiceUtil.GetCustomerDetails(_httpContextAccessor.HttpContext, _cstrepo);
            if (depositingCst == null) return Response.BadRequest(null, $"Customer doesnt exist, please open a wallet.");
            var resp = new BillerResponseDTO();
            var spendMoneyOn = reqModel.BillerService == (int)Billers.Dstv ? Billers.Dstv.ToString() :
                               Billers.Airtime.ToString();
            

            if (reqModel == null) return Response.BadRequest(null);
            if (reqModel.PaymentMode == (int)TopUpMode.ThirdParty)
            {

                var paymodel = new SendMoneyDTO
                {
                    AmountToAdd = reqModel.AmountToBePaid
                };
                var reqTo3RDParty = _mock3RDPartyService.CompleteSendMoney(paymodel);
                if (!reqTo3RDParty.Status) return Response.BadRequest(reqTo3RDParty);

                if (reqModel.BillerService == (int)Billers.Dstv)
                {
                    resp = _biller.BillerDstv(reqModel, spendMoneyOn);
                }
                else
                {
                    resp = _biller.BillerAirtime(reqModel, spendMoneyOn);

                }
                if (!resp.Status) return Response.BadRequest(null, $"{resp.Message}");
                
                return Response.Ok(resp, $"{resp.Message}");
            }
            if (reqModel.PaymentMode == (int)TopUpMode.CustomerWallet && !string.IsNullOrEmpty(reqModel.WalletIdToDebit))
            {
                var getCstWallet = await _ptwalletrepo.Get(t => t.WalletId == reqModel.WalletIdToDebit);
                if(getCstWallet==null) return Response.BadRequest(null, $"WalletId not found.");
                if (getCstWallet.BalanceAmount < reqModel.AmountToBePaid) return Response.BadRequest(null, $"Insufficient wallet funds.");

                if (reqModel.BillerService == (int)Billers.Dstv)
                {
                    resp = _biller.BillerDstv(reqModel, spendMoneyOn);
                }
                else
                {
                    resp = _biller.BillerAirtime(reqModel, spendMoneyOn);

                }

                getCstWallet.BalanceAmount -= reqModel.AmountToBePaid;
                var (id, IsSaved)=await _ptwalletrepo.Save();
                if (id > 0)
                {
                    var createtrans = new Transaction
                    {
                        CreatedAt = DateTime.Now,
                        LastUpdated = DateTime.Now,
                        TransRef = $"WPlus_{UtilMethods.GenerateRandomString(6)}",
                        AmountAdded = reqModel.AmountToBePaid,
                        TopUpMode = reqModel.PaymentMode == (int)TopUpMode.ThirdParty ?
                                TopUpMode.ThirdParty.ToString() :
                                TopUpMode.CustomerWallet.ToString(),
                        Status = ResponseStatus.Success.Message,
                        DepositorId = depositingCst.Id,
                        CustomerId = depositingCst.Id,
                        IsWithdrawal = true,
                        Narration= resp.Narration,
                    };
                    await _transrepo.Add(createtrans);
                    await _transrepo.Save();
                    return Response.Ok(resp, $"{resp.Message}");
                    
                }
                return Response.BadRequest(null, $"Something went wrong.");
            }
            return Response.BadRequest(null, $"Something went wrong.");
        }
    }
}
