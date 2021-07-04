﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Chinook.Domain.Supervisor;
using Chinook.Domain.ApiModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Chinook.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IChinookSupervisor chinookSupervisor, ILogger<CustomerController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all Customers",
            Description = "Get all Customers",
            OperationId = "Customer.GetAll",
            Tags = new[] { "CustomerEndpoint"})]
        [Produces(typeof(List<CustomerApiModel>))]
        public async Task<ActionResult<List<CustomerApiModel>>> Get()
        {
            try
            {
                return new ObjectResult(await _chinookSupervisor.GetAllCustomer());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        [SwaggerOperation(
            Summary = "Get specific Customers",
            Description = "Creates specific Customer",
            OperationId = "Customer.GetOne",
            Tags = new[] { "CustomerEndpoint"})]
        [Produces(typeof(CustomerApiModel))]
        public async Task<ActionResult<CustomerApiModel>> Get(int id)
        {
            try
            {
                var customer = await _chinookSupervisor.GetCustomerById(id);
                if ( customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("supportrep/{id}")]
        [SwaggerOperation(
            Summary = "Get Customers by Support Rep",
            Description = "Get Customers by Support Rep",
            OperationId = "Customer.GetBySupportRep",
            Tags = new[] { "CustomerEndpoint"})]
        [Produces(typeof(List<CustomerApiModel>))]
        public async Task<ActionResult<CustomerApiModel>> GetBySupportRepId(int id)
        {
            try
            {
                var rep = await _chinookSupervisor.GetEmployeeById(id);

                return Ok(rep);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new Customer",
            Description = "Creates a new Customer",
            OperationId = "Customer.Create",
            Tags = new[] { "CustomerEndpoint"})]
        public async Task<ActionResult<CustomerApiModel>> Post([FromBody] CustomerApiModel input)
        {
            try
            {
                if (input == null)
                {
                    return BadRequest("Customer is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Customer object");
                }
                
                var customer = await _chinookSupervisor.AddCustomer(input);
        
                return CreatedAtRoute("GetCustomerById", new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update Customer",
            Description = "Update Customer",
            OperationId = "Customer.Update",
            Tags = new[] { "CustomerEndpoint"})]
        public async Task<ActionResult<CustomerApiModel>> Put(int id, [FromBody] CustomerApiModel input)
        {
            try
            {
                if (input == null)
                {
                    return BadRequest("Customer is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Customer object");
                }

                if (await _chinookSupervisor.UpdateCustomer(input))
                {
                    return CreatedAtRoute("GetCustomerById", new { id = input.Id }, input);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete Customer",
            Description = "Delete Customer",
            OperationId = "Customer.Delete",
            Tags = new[] { "CustomerEndpoint"})]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (await _chinookSupervisor.DeleteCustomer(id))
                {
                    return Ok();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}