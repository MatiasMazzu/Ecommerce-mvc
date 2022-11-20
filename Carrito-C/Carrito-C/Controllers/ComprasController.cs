﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Carrito_C.Data;
using Carrito_C.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Carrito_C.Controllers
{
    public class ComprasController : Controller
    {
        private readonly CarritoCContext _context;
        private readonly UserManager<Persona> _usermanager;

        public ComprasController(UserManager<Persona> usermanager, CarritoCContext context)
        {
            _context = context;
            _usermanager = usermanager;
        }

        // Realiza la compra luego de elegir la sucursal
        public async Task<IActionResult> RealizarCompra(int sucursalId)
        {
            int userId = Int32.Parse(_usermanager.GetUserId(User));
            Cliente cliente = await _context.Clientes.Where(c => c.Id == userId)
                .FirstOrDefaultAsync();
            Carrito carrito = await _context.Carritos.Where(c => c.ClienteId == userId)
                .Include(c => c.Cliente)
                .Include(c => c.CarritoItems)
                .ThenInclude(c => c.Producto)
                .FirstOrDefaultAsync();
            Sucursal sucursal = await _context.Sucursales
                .Where(s => s.Id == sucursalId)
                .Include(s => s.ProductosSucursal)
                .FirstOrDefaultAsync();

            if (carrito != null && carrito.CarritoItems.Count() > 0)
            {
                bool hayStock = ValidarStock(carrito, sucursal);
                if (hayStock)
                {
                    Compra compra = new Compra()
                    {
                        ClienteId = userId,
                        Cliente = cliente,
                        CarritoId = carrito.Id,
                        SucursalId = sucursal.Id,
                        Sucursal = sucursal
                    };
                    await AgregarCompra(compra);

                    foreach (CarritoItem carritoItem in _context.CarritoItems.Where(i => i.CarritoId == carrito.Id))
                    {
                        ComprasItem compraItem = new ComprasItem()
                        {
                            CompraId = compra.Id,
                            Compra = compra,
                            ProductoId = carritoItem.ProductoId,
                            Producto = carritoItem.Producto,
                            Cantidad = carritoItem.Cantidad,
                            Subtotal = carritoItem.Subtotal,
                        };
                        _context.ComprasItems.Add(compraItem);
                        _context.CarritoItems.Remove(carritoItem);
                    }
                    compra.Total = CalcularTotal(compra);
                    _context.Carritos.Update(carrito);
                    _context.Compras.Update(compra);
                    ViewBag.Compra = compra;
                    await _context.SaveChangesAsync();
                    return View("ResumenCompra", compra.ComprasItems);
                }
                else
                {
                    List<Sucursal> sucursales = BuscarSucursalesConStock(carrito);
                    ViewBag.hayStock = hayStock;
                    return View("SeleccionLocal", sucursales);
                }
            }
            else
            {
                return View("Error");
            }
        }

        private List<Sucursal> BuscarSucursalesConStock(Carrito carrito)
        {
            List<Sucursal> allSucursales = _context.Sucursales
                .Include(s => s.ProductosSucursal).ToList();
            List<Sucursal> sucursalesConStock = new List<Sucursal>();
            foreach (Sucursal sucursal in allSucursales)
            {
                if (ValidarStock(carrito, sucursal))
                {
                    sucursalesConStock.Add(sucursal);
                }
            }
            return sucursalesConStock;
        }

        private double CalcularTotal(Compra compra)
        {
            double total = 0;
            foreach (ComprasItem item in compra.ComprasItems)
            {
                total += item.Subtotal;
            }
            return total;
        }

        private bool ValidarStock(Carrito carrito, Sucursal sucursal)
        {
            bool stockOk = true;
            foreach (CarritoItem item in carrito.CarritoItems)
            {
                StockItem stock = sucursal.ProductosSucursal.FirstOrDefault(s => s.ProductoId == item.ProductoId);
                if (stock.Cantidad < item.Cantidad)
                {
                    stockOk = false;
                }
            }
            return stockOk;
        }

        private async Task AgregarCompra(Compra compra)
        {
            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();
        }

        // Muestra todas las sucursales disponibles
        [Authorize(Roles = ("Cliente"))]
        public IActionResult SeleccionLocal()
        {
            var sucursales = _context.Sucursales.ToList();
            return View("SeleccionLocal", sucursales);
        }

    }

}
