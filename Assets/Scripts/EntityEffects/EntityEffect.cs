using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityEffect{
	public float duration = Mathf.Infinity;//total time effect should be applied
	public float age = 0;//how long effect has been applied
	public bool effectEnded = false;
	public bool unique = false; //whether multiple number of effects with same name can be applied at once
	private bool _canceled = false; //if canceled, it wont check age anymore
	public string name;

	protected Entity _targetedEntity;

	public EntityEffect(Entity targetedEntity)
	{
		this._targetedEntity = targetedEntity;
	}

	public EntityEffect(Entity targetedEntity, bool unique)
	{
		this._targetedEntity = targetedEntity;
		this.unique = unique;
	}

	public void Update(){
		UpdateCooldown();
		if(age < duration && !_canceled){
			effectEnded = false;
			// Debug.Log("applying effect");
			UpdateEffect();
		}else{
			// Debug.Log("effect finished");
			OnEffectEnd();
			effectEnded = true;
		}
	}

	public void CancelEffect() 
	{
		_canceled = true;
	}

	public Entity GetTargetedEntity(){
		return _targetedEntity;
	}
	
	void UpdateCooldown() 
	{
		age += Time.deltaTime;     
	}

	public virtual void OnEffectApplied(){}
	//this is for unique effects, when the new effect is replacing the old
	public virtual void OnEffectReapplied(EntityEffect oldEffect){}
	public virtual void UpdateEffect(){}
	public virtual void OnEffectEnd(){}

}