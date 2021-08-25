using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repo;
        private readonly IMapper _mapper;
        public CommandsController(ICommanderRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        } 
        //Get api/commands
        [HttpGet]
        public ActionResult <IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = _repo.GetAllCommand();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        //Get api/commands/{id}
        [HttpGet("{id}")]
        public ActionResult <Command> GetCommandById(int id)
        {
            var commandItem = _repo.GetCommandById(id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();
        }

        //Post  api/commands
        [HttpPost]
        public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repo.CreateCommand(commandModel);
            _repo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id }, commandReadDto);
            // return Ok(commandReadDto);
        }

        // Put  api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(CommandUpdateDto updateDto, int id)
        {
            var commandModel = _repo.GetCommandById(id);
            if(commandModel == null)
            {
                return NotFound();
            }
            _mapper.Map(updateDto, commandModel);
            _repo.UpdateCommand(commandModel);

            _repo.SaveChanges();

            return NoContent();
        }

        // Patch api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModel = _repo.GetCommandById(id);
            if (commandModel == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModel);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(commandToPatch, commandModel);

            _repo.SaveChanges();
            return NoContent();
        }

        //DELETE api/commands/(id)
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModel = _repo.GetCommandById(id);
            if (commandModel == null)
            {
                return NotFound();
            }
            _repo.DeleteCommand(commandModel);

            _repo.SaveChanges();

            return NoContent();
        }
    }
}
