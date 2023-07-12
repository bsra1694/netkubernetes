using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetKubernets.Data.Inmuebles;
using NetKubernets.Dtos.ImuebleDtos;
using NetKubernets.Middleware;
using NetKubernets.Models;

namespace NetKubernets.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InmuebleController : ControllerBase{
    private readonly IImuebleRepository _repository;
    private IMapper _mapper;
    public InmuebleController(IImuebleRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InmuebleResponseDto>>> GetInmuebles()
    {
        var inmuebles = await _repository.GetAllInmuebles();
        return Ok(_mapper.Map<IEnumerable<InmuebleResponseDto>>(inmuebles));
    }

    [HttpGet("{id}", Name = "GetInmuebleById")]
    public async Task<ActionResult<InmuebleResponseDto>> GetInmuebleById(int id)
    {
        var inmueble = await _repository.GetInmuebleById(id);
        if(inmueble is null)
        {
            throw new MiddlewareException(HttpStatusCode.NotFound,
            new {mensaje = $"No se encontró el inmueble por este id {id}"});
        }

        return Ok(_mapper.Map<InmuebleResponseDto>(inmueble));
    }

    [HttpPost]
    public async Task<ActionResult<InmuebleResponseDto>> CreateInmueble([FromBody] InmuebleRequestDto inmueble)
    {
        var inmuebleModel = _mapper.Map<Inmueble>(inmueble);
        await _repository.CreateInmueble(inmuebleModel);
        await _repository.SaveChanges();

        var inmuebleResponse = _mapper.Map<InmuebleResponseDto>(inmuebleModel);

        return CreatedAtRoute(nameof(GetInmuebleById), new {inmuebleResponse.Id}, inmuebleResponse);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteInmueble(int id)
    {
        await _repository.DeleteInmueble(id);
        await _repository.SaveChanges();
        return Ok();
    }

}