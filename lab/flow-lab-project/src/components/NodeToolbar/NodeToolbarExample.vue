<script lang="ts" setup>
import { ref } from 'vue'
import { Position, VueFlow } from '@vue-flow/core'
import CustomNode from './CustomNode.vue'

const defaultNodeStyle = {
  border: '2px solid #ff0071',
  background: 'white',
  borderRadius: '20px',
}

const elements = ref([
  {
    id: '1',
    type: 'custom',
    data: { toolbarPosition: Position.Top, label: 'toolbar top', },
    position: { x: 200, y: 0 },
    style: defaultNodeStyle,
  },
  {
    id: '2',
    type: 'custom',
    data: { toolbarPosition: Position.Right, label: 'toolbar right', },
    position: { x: -50, y: 100 },
    style: defaultNodeStyle,
  },
  {
    id: '3',
    type: 'custom',
    data: { toolbarPosition: Position.Bottom, label: 'toolbar bottom', },
    position: { x: 0, y: 200 },
    style: defaultNodeStyle,
  },
  {
    id: '4',
    type: 'custom',
    data: { toolbarPosition: Position.Left, label: 'toolbar left', },
    position: { x: 200, y: 300 },
    style: defaultNodeStyle,
  },
  {
    id: '5',
    type: 'custom',
    data: { toolbarPosition: Position.Top, toolbarVisible: true, label: 'toolbar always open' },
    position: { x: 0, y: -100 },
    style: defaultNodeStyle,
  },
])
</script>

<template>
  <VueFlow v-model="elements" fit-view-on-init class="vue-flow-basic-example">
    <template #node-custom="nodeProps">
      <CustomNode :data="nodeProps.data" />
    </template>
  </VueFlow>
</template>
