using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class LinkRuleTile : RuleTile<LinkRuleTile.Neighbor> {
    public TileBase[] linkTo;

    public class Neighbor : RuleTile.TilingRule.Neighbor {
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.This:
                foreach (TileBase t in linkTo) {
                    if (tile == t) return true;
                }
                return false;
        }
        return base.RuleMatch(neighbor, tile);
    }
}