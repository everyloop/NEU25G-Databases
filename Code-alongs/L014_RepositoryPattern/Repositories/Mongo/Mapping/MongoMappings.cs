using L014_RepositoryPattern.Domain;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L014_RepositoryPattern.Repositories.Mongo.Mapping;

internal class MongoMappings
{
    private static bool _registered = false;

    public static void Register()
    {
        if (_registered) return;

        var pack = new ConventionPack
        {
            new IgnoreExtraElementsConvention(true)
        };

        ConventionRegistry.Register("appConventions", pack, _ => true);

        // Movie mapping

        BsonClassMap.RegisterClassMap<Movie>(cm =>
        {
            cm.AutoMap();

            cm.MapIdMember(x => x.Id).SetElementName("_id");

            cm.MapMember(x => x.Title).SetElementName("title");
            cm.MapMember(x => x.Year).SetElementName("year");
            cm.MapMember(x => x.Length).SetElementName("runtime");
            cm.MapMember(x => x.Plot).SetElementName("fullplot");
            cm.MapMember(x => x.Imdb).SetElementName("imdb");
        });

        // ImdbInfo mapping (subdocument)
        BsonClassMap.RegisterClassMap<ImdbInfo>(cm =>
        {
            cm.AutoMap();

            cm.MapMember(x => x.ImdbId).SetElementName("id");
            cm.MapMember(x => x.Rating).SetElementName("rating");
            cm.MapMember(x => x.Votes).SetElementName("votes");
        });

        _registered = true;
    }
}
