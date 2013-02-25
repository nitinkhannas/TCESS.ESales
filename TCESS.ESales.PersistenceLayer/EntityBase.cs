using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TCESS.ESales.PersistenceLayer
{
    public abstract class EntityBase : IDisposable
    {
        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}