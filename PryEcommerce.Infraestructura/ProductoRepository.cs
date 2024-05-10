using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using Firebase.Auth;
using Firebase.Storage;
using PryEcommerce.Entidades;
using PryEcommerce.Infraestructura.Util;

namespace PryEcommerce.Infraestructura;

public class ProductoRepository : IGenericRepository<Producto>
{
    private string connectionString;

    public ProductoRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<Producto> FiltrarProducto(
        string nombre,
        string descripcion,
        int categoria_id,
        int marca_id,
        decimal precio_min,
        decimal precio_max
        )
    {
        var list = new List<Producto>();
        using (SqlDataReader dr = SqlHelper.ExecuteReader(connectionString, 
                   "sp_FiltrarProductos", nombre, descripcion, categoria_id, marca_id, precio_min, precio_max))
        {
            while (dr.Read())
            {
                list.Add(new Producto()
                {
                    id = dr.GetInt32(0),
                    nombre = dr.GetString(1),
                    descripcion = dr.GetString(2),
                    url_img = dr.GetString(3),
                    stock = dr.GetInt32(4),
                    precio = dr.GetDecimal(5),
                    estado = dr.GetBoolean(6),
                    fecha_ingreso = dr.GetDateTime(7),
                    categoria = new Categoria()
                    {
                        id = dr.GetInt32(8),
                        nombre = dr.GetString(10)
                    },
                    marca = new Marca()
                    {
                        id = dr.GetInt32(9),
                        nombre = dr.GetString(11)
                    }
                });
            }
        }
        return list;
    }
    
    public List<Producto> Listar()
    {
        var list = new List<Producto>();
        using (SqlDataReader dr = SqlHelper.ExecuteReader(connectionString, "sp_ListarProductos"))
        {
            while (dr.Read())
            {
                list.Add(new Producto()
                {
                    id = dr.GetInt32(0),
                    nombre = dr.GetString(1),
                    descripcion = dr.GetString(2),
                    url_img = dr.GetString(3),
                    stock = dr.GetInt32(4),
                    precio = dr.GetDecimal(5),
                    estado = dr.GetBoolean(6),
                    fecha_ingreso = dr.GetDateTime(7),
                    categoria = new Categoria()
                    {
                        id = dr.GetInt32(8),
                        nombre = dr.GetString(11)
                    },
                    marca = new Marca()
                    {
                        id = dr.GetInt32(9),
                        nombre = dr.GetString(14)
                    }
                });
            }
        }
        return list;
    }

    public Producto Buscar(int id)
    {
        Producto producto = null;
        using (SqlDataReader dr = SqlHelper.ExecuteReader(connectionString, "sp_BuscarProducto", id))
        {
            while (dr.Read())
            {
                producto = new Producto()
                {
                    id = dr.GetInt32(0),
                    nombre = dr.GetString(1),
                    descripcion = dr.GetString(2),
                    url_img = dr.GetString(3),
                    stock = dr.GetInt32(4),
                    precio = dr.GetDecimal(5),
                    estado = dr.GetBoolean(6),
                    fecha_ingreso = dr.GetDateTime(7),
                    categoria = new Categoria()
                    {
                        id = dr.GetInt32(8),
                        nombre = dr.GetString(10)
                    },
                    marca = new Marca()
                    {
                        id = dr.GetInt32(9),
                        nombre = dr.GetString(11)
                    }
                };
            }
        }
        return producto;
    }

    public string Guardar(Producto t)
    {
        SqlHelper.ExecuteNonQuery(connectionString, "sp_AgregarProducto", 
            t.nombre, t.descripcion, t.url_img, t.stock, t.precio, t.categoria.id, t.marca.id);
        string message = "El producto fue agregado con exito";
        return message;
    }

    public string Editar(Producto t)
    {
        SqlHelper.ExecuteNonQuery(connectionString, "sp_ActualizarProducto", 
             t.id, t.nombre, t.descripcion, t.url_img, t.stock, t.precio, t.categoria.id, t.marca.id);
        string message = "El producto fue actualizado con exito";
        return message;
    }

    public string Eliminar(int id)
    {
        string message;
        try
        {
            SqlHelper.ExecuteNonQuery(connectionString, "sp_EliminarProducto", id);
            message = "El producto fue eliminado con exito";
        }
        catch (Exception e)
        {
            message = "El producto no se puede eliminar";
        }
        return message;
    }

    public string Desactivar(int id)
    {
        SqlHelper.ExecuteNonQuery(connectionString, "sp_EstadoProducto", id, false);
        string message = "El producto fue desactivado con exito";
        return message;
    }

    public string Activar(int id)
    {
        SqlHelper.ExecuteNonQuery(connectionString, "sp_EstadoProducto", id, true);
        string message = "El producto fue activado con exito";
        return message;
    }

    public async Task<string> SubirImagen(Stream arch, string name)
    {
        string email = "jackfrancomoreno15@gmail.com";
        string password = "codigo123";
        string root = "corefirebase-a624e.appspot.com";
        string api_key = "AIzaSyBz_gggrpUFCGxKiB9ebfPFAuGMKQ1RdkU";

        var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
        var a = await auth.SignInWithEmailAndPasswordAsync(email, password);
        
        var cancellation = new CancellationTokenSource();

        var task = new FirebaseStorage(
                root,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                }
            )
            .Child("Fotos")
            .Child(name)
            .PutAsync(arch, cancellation.Token);
        
        var downloadURL = await task;
        
        return downloadURL;

    }

}