﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace Domain.Abstractions;

public interface IActionTransactionHelper
{
    void BeginTransaction();
    void EndTransaction(ActionExecutedContext filterContext);
    void CloseSession();
}
