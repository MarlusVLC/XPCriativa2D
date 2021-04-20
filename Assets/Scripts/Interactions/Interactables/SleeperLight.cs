﻿using System;
using System.Collections;
using System.Collections.Generic;
using Aux_Classes;
using UnityEngine;

namespace Interactions.Interactables

{
    public class SleeperLight : Light
    {

    
    
        public override void Trigger()
        {
            if (_canPress)
            {
                // _affectedAnim.SetTrigger("On");  //> Isso agora é responsabilidade do responder
                Interact();
                _anim.SetTrigger("Defunct");
            }    
        }
    }
}

