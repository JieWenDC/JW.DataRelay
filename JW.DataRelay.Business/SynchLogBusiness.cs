using JW.DataRelay.Entity;
using JW.DataRelay.Provider;

namespace JW.DataRelay.Business
{
    public class SynchLogBusiness
    {
        public SynchLogProvider SynchLogProvider { get; set; }

        public SynchLogBusiness() : base()
        {
            SynchLogProvider = new SynchLogProvider();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        public void Add(SynchLog entity)
        {
            SynchLogProvider.Add(entity);
        }
    }
}
