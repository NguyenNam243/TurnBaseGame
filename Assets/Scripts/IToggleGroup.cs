using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IToggleGroup<T1, T2> : MonoBehaviour where T1 : IToggle<T2> where T2 : class
{
    [Header("Object Reference")]
    [SerializeField] private GameObject togglePrefab = null;

    protected List<IToggle<T2>> toggles = new List<IToggle<T2>>();


    public bool AnyCardOn => toggles.Any(g => g.Selected);
    public T2 CurrentDataSelected => CurrentToggleOn?.Data;
    public IToggle<T2> CurrentToggleOn => toggles.Find(g => g.Selected);




    public virtual void Initialized(Dictionary<string, T2> datas)
    {
        foreach (var data in datas)
        {
            GameObject obj = Instantiate(togglePrefab, transform);
            IToggle<T2> toggleScript = obj.GetComponent<IToggle<T2>>();
            toggles.Add(toggleScript);
            toggleScript.OnSelected = OnCardSelected;
            toggleScript.Initialized(data.Value);
        }
    }

    protected virtual void OnCardSelected(T2 Data)
    {

    }
}
