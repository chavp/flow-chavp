<script lang="ts" setup>
import { ref } from 'vue'

import type { Edge, EdgeChange, Node, NodeChange } from '@vue-flow/core'
import { VueFlow, useVueFlow } from '@vue-flow/core'
import { Background } from '@vue-flow/background'
import { useDialog } from './useDialog'
import Dialog from './Dialog.vue'

const { onConnect, addEdges, onNodesChange, onEdgesChange, applyNodeChanges, applyEdgeChanges } = useVueFlow()

const dialog = useDialog({ message: 'Do you really want to delete this item?' })

const nodes = ref<Node[]>([
  { id: '1', type: 'input', label: 'Node 1', position: { x: 250, y: 5 }, class: 'light' },
  { id: '2', label: 'Node 2', position: { x: 100, y: 100 }, class: 'light' },
  { id: '3', label: 'Node 3', position: { x: 400, y: 100 }, class: 'light' },
  { id: '4', label: 'Node 4', position: { x: 400, y: 200 }, class: 'light' },
])

const edges = ref<Edge[]>([
  { id: 'e1-2', source: '1', target: '2', animated: true },
  { id: 'e1-3', source: '1', target: '3' },
])

onConnect(addEdges)

onNodesChange(async (changes) => {
  const nextChanges: NodeChange[] = []

  for (const change of changes) {
    if (change.type === 'remove') {
      const isConfirmed = await dialog.confirm()

      if (isConfirmed) {
        nextChanges.push(change)
      }
    } else {
      nextChanges.push(change)
    }
  }

  applyNodeChanges(nextChanges)
})

onEdgesChange(async (changes) => {
  const nextChanges: EdgeChange[] = []

  for (const change of changes) {
    if (change.type === 'remove') {
      const isConfirmed = await dialog.confirm()

      if (isConfirmed) {
        nextChanges.push(change)
      }
    } else {
      nextChanges.push(change)
    }
  }

  applyEdgeChanges(nextChanges)
})
</script>

<template>
    <div class="flow-editor">
        <div class="flow-canvas">
        <VueFlow 
            :nodes="nodes" 
            :edges="edges" 
            :apply-default="false" 
            fit-view-on-init 
            class="vue-flow-basic-example">
            <Background />
            <Dialog />
        </VueFlow>
        </div>
    </div>
</template>

<style>
/* import the necessary styles for Vue Flow to work */
@import '@vue-flow/core/dist/style.css';

/* import the default theme, this is optional but generally recommended */
@import '@vue-flow/core/dist/theme-default.css';
@import '@vue-flow/minimap/dist/style.css';

/* import default controls styles */
@import '@vue-flow/controls/dist/style.css';

.flow-editor {
  width: 100vw;
  height: 100vh;
  position: fixed;
  top: 0;
  left: 0;
}

/* Dark theme support */
@media (prefers-color-scheme: dark) {
  .flow-canvas {
    width: 100%;
    height: 100%;
    background: #0f1419;
    background-image: 
      radial-gradient(circle, #374151 1px, transparent 1px);
    background-size: 20px 20px;
  }
}
</style>