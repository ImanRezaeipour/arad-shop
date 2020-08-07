namespace Shopping.Repository.Write.Interface
{
    public interface ISeqRepository
    {
        long GetNextSequenceValue(string seqName);
    }
}