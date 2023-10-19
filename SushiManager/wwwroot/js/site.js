// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function logout()
{
    const confirmLogout = confirm("Sei sicuro di voler effettuare il logout ?");

    if (confirmLogout)
    {
        $.ajax({
            url: "/UserLogin/Logout",
            type: "POST",
            success: function (result)
            {
                alert("Logout effettuato!")

                window.location.href = '/Home/Index';
            },
            error: function (xhr, status, error)
            {
                alert("Errore in fase di logout, riprova più tardi!")

                window.location.href = '/Home/Index';
            }
        });
    }
}
