using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaVaxi
{
    public class CuentaBancaria
    {
        public int Balance { get; set; }
        private readonly ILoggerGeneral _loggerGeneral;
        
        public CuentaBancaria(ILoggerGeneral loggerGeneral)
        {
            Balance = 0;
            _loggerGeneral = loggerGeneral;
        }

        public bool Deposito(int monto)
        {
            _loggerGeneral.Message("Esta depositando la cantidad de " + monto);
            _loggerGeneral.Message("Es otro texto");
            _loggerGeneral.Message("visita vaxidrez.com");
            _loggerGeneral.PrioridadLogger = 100;
            var prioridad = _loggerGeneral.PrioridadLogger;

            Balance += monto;
            return true;
        }

        public bool Retiro(int monto)
        {
            if(monto <= Balance)
            {
                _loggerGeneral.LogDatabase("Monto de retiro:" + monto.ToString());
                Balance -= monto;
                return _loggerGeneral.LogBalanceDespuesRetiro(Balance);
            }
                
            return _loggerGeneral.LogBalanceDespuesRetiro(Balance - monto);
        }
        
        public int GetBalance()
        {
            return Balance;
        }
    }
}
