public class Player : Entity
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        ActionController = new PlayerActionController(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
