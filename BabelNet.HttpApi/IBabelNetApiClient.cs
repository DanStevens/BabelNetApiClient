using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BabelNet.HttpApi
{
    public interface IBabelNetApiClient
    {
        Task<ICollection<SynsetRelation>> GetOutgoingEdgesAsync(string id);
        Task<ICollection<SynsetRelation>> GetOutgoingEdgesAsync(string id, CancellationToken cancellationToken);
        Task<ICollection<Sense>> GetSensesAsync(string lemma, string searchLang, CancellationToken cancellationToken = default);
        Task<ICollection<Sense>> GetSensesAsync(string lemma, string searchLang, IEnumerable<string> targetLang, UniversalPOS? pos, string source);
        Task<ICollection<Sense>> GetSensesAsync(string lemma, string searchLang, IEnumerable<string> targetLang, UniversalPOS? pos, string source, CancellationToken cancellationToken);
        Task<ICollection<Sense>> GetSensesAsync(string lemma, string searchLang, string targetLang, UniversalPOS? pos = null, string? source = null, CancellationToken cancellationToken = default);
        Task<Synset> GetSynsetAsync(string id, IEnumerable<string> targetLang);
        Task<Synset> GetSynsetAsync(string id, IEnumerable<string> targetLang, CancellationToken cancellationToken);
        Task<ICollection<SynsetId>> GetSynsetIdsAsync(string lemma, IEnumerable<string> searchLangs);
        Task<ICollection<SynsetId>> GetSynsetIdsAsync(string lemma, IEnumerable<string> searchLang, IEnumerable<string> targetLang, UniversalPOS? pos, string source);
        Task<ICollection<SynsetId>> GetSynsetIdsAsync(string lemma, IEnumerable<string> searchLang, IEnumerable<string> targetLang, UniversalPOS? pos, string source, CancellationToken cancellationToken);
        Task<ICollection<SynsetId>> GetSynsetIdsAsync(string lemma, string searchLang, CancellationToken cancellationToken = default);
        Task<ICollection<SynsetId>> GetSynsetIdsAsync(string lemma, string searchLang, string targetLang, UniversalPOS? pos = null, string? source = null, CancellationToken cancellationToken = default);
        Task<ICollection<Synset>> GetSynsetIdsFromResourceIDAsync(string id, string source, string searchLang, IEnumerable<string> targetLang, UniversalPOS? pos, string wnVersion);
        Task<ICollection<Synset>> GetSynsetIdsFromResourceIDAsync(string id, string source, string searchLang, IEnumerable<string> targetLang, UniversalPOS? pos, string wnVersion, CancellationToken cancellationToken);
        Task<Response> GetVersionAsync();
        Task<Response> GetVersionAsync(CancellationToken cancellationToken);
    }
}