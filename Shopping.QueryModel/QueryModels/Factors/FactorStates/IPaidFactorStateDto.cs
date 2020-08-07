using System;

namespace Shopping.QueryModel.QueryModels.Factors.FactorStates
{
    public interface IPaidFactorStateDto : IFactorStateBaseDto
    {
        DateTime PayTime { get; set; }
        string ReferenceId { get; set; }
        string TransactionId { get; set; }
        string Message { get; set; }
    }
}