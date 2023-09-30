using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class UsuarioController : Controller
{
    private readonly ApplicationDbContext _context;

    public UsuarioController(ApplicationDbContext context)
    {
        _context = context;
    }

    //// Listar top 10 usuarios de la base antes creada (10 puntos)
    public IActionResult ListarTop10Usuarios()
    {
        var usuariosTop10 = _context.Usuarios
            .OrderByDescending(u => u.Sueldo)
            .Take(10)
            .ToList();

        return View(usuariosTop10);
    }

    // Generar un archivo csv con las siguienets campos con su información(Login, Nombre completo, sueldo, fecha Ingreso) (25 puntos)
    public IActionResult GenerarCSV()
    {
        var usuarios = _context.Usuarios.ToList();

        var csvData = "Login,Nombre Completo,Sueldo,Fecha de Ingreso\n";
        foreach (var usuario in usuarios)
        {
            csvData += $"{usuario.Login},{usuario.NombreCompleto},{usuario.Sueldo},{usuario.FechaIngreso}\n";
        }

        var csvBytes = System.Text.Encoding.UTF8.GetBytes(csvData);

        return File(csvBytes, "text/csv", "usuarios.csv");
    }

    // Poder actualizar el salario del algun usuario especifico (10 puntos)
    public IActionResult ActualizarSalario(int id, decimal nuevoSalario)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);

        if (usuario != null)
        {
            usuario.Sueldo = nuevoSalario;
            _context.SaveChanges();
        }

        return RedirectToAction("ListarTop10Usuarios");

    }

    // Poder Tener una opcion para agregar un nuevo usuario y se pueda asiganar el salario y 
    //la fecha de ingreso por default el dia de hoy (25 puntos)
    [HttpGet]
    public IActionResult AgregarUsuario()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AgregarUsuario(Usuario nuevoUsuario)
    {
        if (ModelState.IsValid)
        {
            nuevoUsuario.FechaIngreso = DateTime.Today;
            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();

            return RedirectToAction("ListarTop10Usuarios");
        }

        return View(nuevoUsuario);
    }


}
