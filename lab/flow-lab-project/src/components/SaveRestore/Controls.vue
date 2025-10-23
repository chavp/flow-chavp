<script lang="ts" setup>
import type { Edge, FlowExportObject, Node } from '@vue-flow/core'
import { useVueFlow } from '@vue-flow/core'
import { defineStore } from 'pinia'

const flowKey = 'example-flow'
const useStateStore = defineStore(flowKey, {
  state: () => (
      { data: {
          nodes: [] as Node[],
          edges: [] as Edge[],
          viewport: { x: 0, y: 0, zoom: 1 }
        } as FlowExportObject
      }
    ),
  getters: {
    value: (state) => state.data
  },
  actions: {
    update(data:FlowExportObject) {
      this.$state.data = data
    },
  }
})

const stateStore = useStateStore()

function getNodeId() {
  return `randomnode_${+new Date()}`
}

const { addNodes, setNodes, setEdges, toObject, dimensions, setViewport } = useVueFlow(flowKey)

function onSave() {
  stateStore.update(toObject())
}

function onRestore() {
  const flow = stateStore.value

  if (flow) {
    const [x = 0, y = 0] = flow.position

    setNodes(flow.nodes)

    setEdges(flow.edges)

    setViewport({ x, y, zoom: flow.zoom || 0 })
  }
}

function onAdd() {
  addNodes({
    id: `random_node-${getNodeId()}`,
    label: 'Added node',
    position: { x: Math.random() * dimensions.value.width, y: Math.random() * dimensions.value.height },
  })
}

</script>

<template>
  <div class="save__controls">
    <button @click="onSave">save</button>
    <button @click="onRestore">restore</button>
    <button @click="onAdd">add node</button>
  </div>
</template>
