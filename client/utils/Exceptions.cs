using System;
// Exceções personalizadas do cliente
namespace Exceptions
{
    public class TitleIsTooLongException : Exception
    {
        public TitleIsTooLongException() : base("O título é muito grande. Não foi possivel")
        {
            
        }
    }
}