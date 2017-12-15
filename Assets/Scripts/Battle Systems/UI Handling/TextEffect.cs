using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/********************************************
 * Text Effect class
 * 
 * this class can be put on any text ui element to allow a custom transition movement or color change
 * 
 * Im soley working on this and its pretty simple so im not gonna comment it ask justin if anything needs clarification
 * 
 */
public class TextEffect : MonoBehaviour {

 
    public bool _colorTransition = false;
    public Color _mainColor, _secondaryColor;

    public bool _fade = false;
    public float _fadeOffset;
    public float _fadeDuration;
    private float alpha;

    public bool _movement = false;
    public Vector2 _movementDirection;
    public float _movementOffset;
    public float _movementSpeed;
    

    public Text _text;
    private float Timer;
    
    void Start()
    {
        _text.color = _mainColor;
        alpha = _mainColor.a;
    }
    void Update () {
        Timer += Time.deltaTime;
        if(_movementOffset <= Timer && _movement)
        {
            transform.position = transform.position + new Vector3(_movementDirection.x, _movementDirection.y, 0) * _movementSpeed;
        }
        if(_fadeOffset<= Timer && _fade ==true)
        {
            alpha -= (1f / _fadeDuration) * Time.deltaTime;
            _text.color = new Color(_mainColor.r, _mainColor.g, _mainColor.b, alpha );
        }
        if(alpha <=0)
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string newText) {
        _text.text = newText;
    }
    public void SetColor(Color c) {
        _mainColor = c;
        _text.color = c;
    }
}
