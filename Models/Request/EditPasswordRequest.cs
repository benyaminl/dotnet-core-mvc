using Microsoft.AspNetCore.Mvc;

namespace MvcNet.Models.Request;

public class EditPasswordRequest
{
    [FromForm(Name = "newpass")]
    public string newPassword {get; set;} = "";
    [FromForm(Name = "confirm-newpass")]
    public string confirmNewPassword {get; set;} = "";

    // public bool IsEqual()
    // {
    //     return this.newPassword == this.confirmNewPassword;
    // }
}