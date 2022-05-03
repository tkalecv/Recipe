using System;

namespace Recipe.REST.ViewModels.Error
{
    public class ErrorVM
    {
        public string Message { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
