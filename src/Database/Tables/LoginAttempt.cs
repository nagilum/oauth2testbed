using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace oauth2testbed.Database.Tables
{
    [Table("LoginAttempts")]
    public class LoginAttempt
    {
        #region ORM

        [Key]
        [Column]
        public int Id { get; set; }

        [Column]
        public int ClientDbId { get; set; }

        [Column]
        public string Created { get; set; }

        [Column]
        public string AuthCode { get; set; }

        [Column]
        public string AccessToken { get; set; }

        [Column]
        public int? AccessTokenExpiresIn { get; set; }

        #endregion
    }
}