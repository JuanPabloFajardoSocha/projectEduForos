// * El formulario debe llevar lo siguente: título, descripción, curso, 
// asignatura, estado, fecha inicio, fecha fin, cantidad de respuestas, si es calificable, 
// botón “cambiar estado”, botón “editar”, botón “eliminar” botón “respuestas”.

using System.Reflection.Metadata;

namespace eduForos.Contracts.Forum.Result;

public record GetForumsByUserResponse
(
     int IdForum,
     string NameForum,
     string Description,
     string UrlPhoto,
     string AssetId,
     DateTime StartDate,
     DateTime EndDate

);