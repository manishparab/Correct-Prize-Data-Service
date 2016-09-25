using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPDataService.BusinessLayer
{
    public class BaseBusiness : IDisposable
    {
        protected CPEntities context;

        public BaseBusiness()
        {
            this.context =  new CPEntities();
        }
        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}