using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.ViewModel
{
    public class ClientBatchUpdateViewModel
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string LastName { get; set; }
    }
}