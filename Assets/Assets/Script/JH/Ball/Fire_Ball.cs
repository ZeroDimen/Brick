
public class Fire_Ball : Ball
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        Destroy_Ball();
    }
}
