<script lang="ts" setup>
import { Handle, Position, useVueFlow } from '@vue-flow/core'
import { computed } from 'vue'

const props = defineProps<{ id: string }>()

const { connectionStartHandle } = useVueFlow()

const isTarget = computed(() => connectionStartHandle.value && connectionStartHandle.value.nodeId !== props.id)
</script>

<template>
  <div class="customNode">
    <div
      class="customNodeBody"
      :style="{
        borderStyle: isTarget ? 'dashed' : 'solid',
        backgroundColor: isTarget ? '#ffcce3' : '#ccd9f6',
      }"
    >
      <Handle class="targetHandle" style="z-index: 2" :position="Position.Right" type="source" connectable />
      <Handle class="targetHandle" :style="{ zIndex: isTarget ? 3 : 1 }" :position="Position.Left" type="target" connectable />
      {{ isTarget ? 'Drop here' : 'Drag to connect' }}
    </div>
  </div>
</template>

<style>
.customNodeBody {
  width: 150px !important;
  height: 80px !important;
  border: 3px solid black !important;
  position: relative !important;
  overflow: hidden !important;
  border-radius: 10px !important;
  display: flex !important;
  justify-content: center !important;
  align-items: center !important;
  font-weight: bold !important;
}

.customNode:before {
  content: '' !important;
  position: absolute !important;
  top: -10px !important;
  left: 50% !important;
  height: 20px !important;
  width: 40px !important;
  transform: translate(-50%, 0) !important;
  background: #d6d5e6 !important;
  z-index: 1000 !important;
  line-height: 1 !important;
  border-radius: 4px !important;
  color: #fff !important;
  font-size: 9px !important;
  border: 2px solid #222138 !important;
}

.sourceHandle {
  width: 100% !important;
  height: 100% !important;
  position: absolute !important;
  top: 0 !important;
  left: 0 !important;
  border-radius: 0 !important;
  transform: none !important;
  border: none !important;
  opacity: 0 !important;
}

.targetHandle {
  width: 100% !important;
  height: 100% !important;
  background: blue !important;
  position: absolute !important;
  top: 0 !important;
  left: 0 !important;
  border-radius: 0 !important;
  transform: none !important;
  border: none !important;
  opacity: 0 !important;
}
</style>