using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // Standart moving path
    private Vector2 direction = Vector2.right;

    // Segments for the growing process
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPreFab;
    public int InitialSize = 4;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        // Control of the movement 
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            direction = Vector2.up;
        }else if(Input.GetKeyDown(KeyCode.DownArrow)){
            direction = Vector2.down;
        }else if(Input.GetKeyDown(KeyCode.RightArrow)){
            direction = Vector2.right;
        }else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            direction = Vector2.left;
        }
    }

    private void FixedUpdate()
    {

        // Loop backwards to move the snake in the right sequence
        for(int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        // Keeps the snake head position updated every frame
        this.transform.position = new Vector3 (
        Mathf.Round(this.transform.position.x) + direction.x, 
        Mathf.Round(this.transform.position.y) + direction.y, 
        0.0f 
        );
    }

    private void Grow()
    {
        // Grow function
       Transform segment = Instantiate(this.segmentPreFab);
       segment.position = _segments[_segments.Count - 1].position; // Adding the last segment to the end of the snake

       _segments.Add(segment);
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++ ){
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        this.transform.position = Vector3.zero;

        for (int i = 1; i < this.InitialSize; i++){
            _segments.Add(Instantiate(this.segmentPreFab));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food") // When snake collide with food it will grow
        {
            Grow();
        } else if (other.tag == "Obstacle"){
            ResetState();
        }
    }
}
