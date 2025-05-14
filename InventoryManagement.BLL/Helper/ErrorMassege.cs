using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.BLL.Helper
{
  public class ErrorMassege
{
    private ErrorMassege(string code, string description, ErrorType errorType)
    {
        Code = code;
        Description = description;
        ErrorType = errorType;
    }

    public string Code { get; }
    public string Description { get; }
    public ErrorType ErrorType { get; }

    public static ErrorMassege Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);

    public static ErrorMassege NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);

    public static ErrorMassege Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);

    public static ErrorMassege Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);

    public static ErrorMassege AccessUnAuthorized(string code, string description) =>
        new(code, description, ErrorType.AccessUnAuthorized);

    public static ErrorMassege AccessForbidden(string code, string description) =>
        new(code, description, ErrorType.AccessForbidden);
}

}
