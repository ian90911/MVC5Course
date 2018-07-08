namespace MVC5Course.Models
{
    using MVC5Course.Models.Attribute;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(ClientMetaData))]
    public partial class Client : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
        {
            if ((Latitude.HasValue && !Longitude.HasValue)
                || (!Latitude.HasValue && Longitude.HasValue))
            {
                yield return new ValidationResult(
                "要填經緯度就經度或緯度都要填，否則就都不要填", new[] { "Latitude", "Longitude" });
            }
        }
    }

    public partial class ClientMetaData
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
        
        [Required]
        [StringLength(1, ErrorMessage="欄位長度不得大於 1 個字元")]
        public string Gender { get; set; }

        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<double> CreditRating { get; set; }
        
        [StringLength(7, ErrorMessage="欄位長度不得大於 7 個字元")]
        public string XCode { get; set; }
        public Nullable<int> OccupationId { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string TelephoneNumber { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string Street1 { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string Street2 { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string City { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string ZipCode { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Latitude { get; set; }
        public string Notes { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        [TaiwanIdentificationNumberAttribute]
        public string IdNumber { get; set; }
    
        public virtual Occupation Occupation { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
