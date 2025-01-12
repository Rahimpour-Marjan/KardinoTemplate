using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public class Enums
    {
    }
    /// <summary>
    /// جنسیت
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// مذکر
        /// </summary>
        Male = 1,
        /// <summary>
        /// مونث
        /// </summary>
        Female = 2
    }
    public enum UserLogType
    {
        Login = 1,

        Logout = 2,
    }


    public enum UserType
    {
        [Display(Name = "یوزر سیستمی")]
        SystemUser = 0,
        [Display(Name = "یوزر داینامیک")]
        DynamicUser = 1,

    }
}
