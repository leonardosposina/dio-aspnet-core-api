using DigitalInnovationOne.CoronavirusAPI.Data.Collections;
using DigitalInnovationOne.CoronavirusAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace DigitalInnovationOne.CoronavirusAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class InfectadoController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infectado> _infectadosCollection;

        public InfectadoController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult CreateInfectado([FromBody] InfectadoDTO dto)
        {
            var infectado = new Infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);

            _infectadosCollection.InsertOne(infectado);

            return StatusCode(201, "Registro adicionado com sucesso!");
        }

        [HttpGet]
        public ActionResult GetAllInfectados()
        {
            var infectados = _infectadosCollection
                .Find(Builders<Infectado>.Filter.Empty)
                .ToList();

            return Ok(infectados);
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult GetOneInfectado([FromRoute] string id)
        {
            var infectado = _infectadosCollection
                .Find(infectado => infectado.Id == id)
                .SingleOrDefault();

            if (infectado == null)
            {
                return NotFound($"Registro de id: {id} não foi encontrado!");
            }

            return Ok(infectado);
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult UpdateInfectado([FromRoute] string id, [FromBody] InfectadoDTO dto)
        {
            var filter = Builders<Infectado>.Filter.Where(infectado => infectado.Id == id);
            var update = Builders<Infectado>.Update
                .Set(Infectado => Infectado.DataNascimento, dto.DataNascimento)
                .Set(Infectado => Infectado.Sexo, dto.Sexo)
                .Set(Infectado => Infectado.Localizacao, new GeoJson2DGeographicCoordinates(dto.Longitude, dto.Latitude));

            var result = _infectadosCollection.UpdateOne(filter, update);

            if (result.ModifiedCount == 0)
            {
                return NotFound($"Registro de id: {id} não foi encontrado!");
            }

            return Ok("Registro atualizado com sucesso!");
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult DeleteInfectado([FromRoute] string id)
        {
            var result = _infectadosCollection.DeleteOne(infectado => infectado.Id == id);

            if (result.DeletedCount == 0)
            {
                return NotFound($"Registro com id: {id} não foi encontrado!");
            }

            return NoContent();
        }

    }
}
