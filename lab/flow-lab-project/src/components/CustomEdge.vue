<script setup lang="ts">
import { computed } from "vue"
import type { EdgeProps, StepEdge } from '@vue-flow/core'
import { BaseEdge, getBezierPath, 
  getSimpleBezierPath, 
  getSmoothStepPath,
  getStraightPath
} from '@vue-flow/core'
import { CustomData } from './edges'
import { Position, Handle, type HandleConnectableFunc } from '@vue-flow/core' 

const handleConnectable: HandleConnectableFunc = (node, connectedEdges) => {
  // only allow connections if the node has less than 3 connections
  return connectedEdges.length < 3
}
// props were passed from the slot using `v-bind="customEdgeProps"`
const props = defineProps<EdgeProps<CustomData>>();

//console.log(props.data.hello) // 'world'

//const edgePathParams = computed(() => getBezierPath({ ...props, curvature: 0.5 }))
const edgePathParams = computed(() => getSmoothStepPath({ ...props, borderRadius: 8 }))
//const edgePathParams = computed(() => getStraightPath({ ...props }))
</script>

<script lang="ts">
export default {
  name: 'CustomEdge',
};
</script>

<template>
  <BaseEdge
      :source-x="sourceX"
      :source-y="sourceY"
      :target-x="targetX"
      :target-y="targetY"
      :source-position="sourcePosition"
      :target-position="targetPosition"

      :path="edgePathParams[0]"
  />
</template>