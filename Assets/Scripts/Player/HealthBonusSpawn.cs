using System.Collections;
using UnityEngine;

public class HealthBonusSpawn : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private BonusHealt _bonus;

    private bool _isCreated = false;

    private void OnEnable()
    {
        _player.DecreasedHalf += Create;
    }

    private void OnDisable()
    {
        _player.DecreasedHalf -= Create;
    }

    private void Create()
    {
        if(_isCreated == false)
        {
            Vector3 transformOffset = new Vector3(Random.Range(1, 10), 0, Random.Range(1, 10));
            Instantiate(_bonus, transform.position + transformOffset, Quaternion.identity);
            _isCreated = true;
            StartCoroutine(UpdateOpportunityGetBonus());
        }        
    }

    private IEnumerator UpdateOpportunityGetBonus()
    {
        WaitForSeconds wait = new WaitForSeconds(40f);
        yield return wait;
        _isCreated = false;

    }
}
