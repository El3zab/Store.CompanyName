﻿using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController(IServicesManager servicesManager) : ControllerBase
    {
        [HttpPost] // POST: /api/Orders
        public async Task<IActionResult> CreateOrder(OrderRequestDto request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await servicesManager.orderService.createOrderAsync(request, email);
            return Ok(result);
        }

        [HttpGet] // GET: /api/Orders
        public async Task<IActionResult> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await servicesManager.orderService.GetOrderByEmailAsync(email);
            return Ok(result);
        }

        [HttpGet("{id}")] // GET: /api/Orders/kjkjk
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var result = await servicesManager.orderService.GetOrderByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("DeliveryMethods")] // GET: /api/Orders/DeliveryMethods
        public async Task<IActionResult> GetAllDeliveryMethods()
        {
            var result = await servicesManager.orderService.GetAllDeliveryMethods();
            return Ok(result);
        }
    }
}
