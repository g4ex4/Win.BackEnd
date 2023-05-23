namespace Win.WebApi
{
    public class Test
    {
        //Закоментировал проверка в dto model 
        //if (!employeeDto.Email.Contains("@"))
        //{
        //    return new EmployeeResponse(400, "Email is not correct", false, null);
        //}

        //if (_context.Employees.Count(x => x.Email.Trim() == employeeDto.Email.Trim()) > 0)
        //{
        //    return new EmployeeResponse(400, "Error, Email already exists", false, null);  //Ошибка, электронная почта уже существует
        //}
    }
}
