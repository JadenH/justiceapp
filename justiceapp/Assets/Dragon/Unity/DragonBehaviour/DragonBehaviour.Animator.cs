using System.Linq;
using DG.Tweening;
using Time = UnityEngine.Time;

public abstract partial class DragonBehaviour
{
    private Sequence _currentAnimation;

    private void InitAnimation()
    {
        OnDestroyed += () => _currentAnimation.Kill();
        var objectState = _gameStates.All(g => g.Is(State.Enabled));

        var animator = this as IDragonAnimator;
        if (animator != null)
        {
            GameStateAnimation(false, animator, true);

            if (objectState)
            {
                GameStateAnimation(true, animator, Time.frameCount <= 1);
            }
        }
        else
        {
            SetCanvasInteractable(objectState);
            gameObject.SetActive(objectState);
        }
    }

    private void GameStateAnimation(bool state, IDragonAnimator animator, bool skipAnimation)
    {
        if (_currentAnimation != null) _currentAnimation.Kill();
        if (state)
        {
            animator.AnimateOut().Complete();
        }
        _currentAnimation = state ? animator.AnimateIn() : animator.AnimateOut();
        _currentAnimation.OnStart(() =>
        {
            if (state)
            {
                gameObject.SetActive(true);
            }
            else
            {
                SetCanvasInteractable(false);
            }
        });
        _currentAnimation.OnComplete(() =>
        {
            if (!state)
            {
                gameObject.SetActive(false);
            }
            else
            {
                SetCanvasInteractable(true);
            }
        });
        if (skipAnimation)
        {
            _currentAnimation.Complete(true);
        }
        else
        {
            _currentAnimation.Play();
        }
    }

    public void AnimateOut(IDragonAnimator animator, bool destroyOnComplete)
    {
        if (_currentAnimation != null) _currentAnimation.Kill();
        _currentAnimation = animator.AnimateOut();
        _currentAnimation.OnStart(() =>
        {
            SetCanvasInteractable(false);
        });
        _currentAnimation.OnComplete(() =>
        {
            if (destroyOnComplete)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        });
    }
}