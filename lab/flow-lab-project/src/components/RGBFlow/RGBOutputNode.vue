<script lang="ts" setup>
import invert from 'invert-color'
import type { NodeProps } from '@vue-flow/core'
import { Handle, Position } from '@vue-flow/core'

interface RBGOutputNodeProps extends NodeProps {
  rgb: string,
  color: Array<number>
}

const props = defineProps<RBGOutputNodeProps>()

</script>

<template>
  <div :style="{ backgroundColor: props.rgb, color: invert(props.color) }" class="rgb-output-node">
    <div class="text-md uppercase">{{ props.rgb }}</div>
    <Handle type="target" :position="Position.Left" />
  </div>
</template>

<style>
.rgb-output-node {
  padding: 25px;
  border-radius: 25px;
  text-align: left;
}
</style>
