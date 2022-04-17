namespace Utils.XUnit
{
    public interface IContextSpecification
    {
        /// <summary>
        /// is called to execute the test
        /// </summary>
        void Because();

        /// <summary>
        /// is called to setup the context for thetest
        /// </summary>
        void EstablishContext();
    }
}