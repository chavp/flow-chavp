<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import type { Node, Edge, FlowExportObject } from '@vue-flow/core'
import { VueFlow, useVueFlow, Panel } from '@vue-flow/core'
import { defineStore } from 'pinia'
import { uuid } from 'vue-uuid';
import axios from 'axios';

const flowKey = '6e1e0c7e-8284-48ee-8114-3640bc3ebe4c'
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
    state: (state) => state.data
  },
  actions: {
    save(data:FlowExportObject) {
      this.$state.data = data
      //var baz:FlowExportObject = JSON.parse(jsonString);
      //var jsonFlow = JSON.stringify(this);
      //console.log(jsonFlow)
    },
  }
})
const stateStore = useStateStore()

const { 
    // nodes
    addNodes,

    toObject,
    setNodes,
    setEdges,
    setViewport
} = useVueFlow(flowKey)

// Functions
function addNode(type: String) {
  const id = uuid.v4()
  
  addNodes({
    id,
    type,
    position: { x: 0, y: 0 },
    data: { label: `Node ${id}`, },
  })
}

function saveState() {
    stateStore.save(toObject())

     axios
        .put('https://localhost:7157/flows/' + flowKey, stateStore)
        .then(response => {
            //console.log(response.data.data);
            stateStore.$id = response.data.id
            stateStore.save(response.data.data)
            restoreState()
            
            alert('TSave flow success!')
        })
        .catch(error => console.log(error));
}

function restoreState() {
  const flow = stateStore.state

  if (flow) {
    const [x = 0, y = 0] = flow.position
    setNodes(flow.nodes)
    setEdges(flow.edges)
    setViewport({ x, y, zoom: flow.zoom || 0 })
  }
}

const connector = (params) => {

  if (params.source === params.target) {
    return false
  }
  
  const id = uuid.v4()

  return {
    id: id,
    source: params.source,
    target: params.target,
    label: `Edge ${id}`,
    animated: false,
  }
}

onMounted(async () => {
  await axios
    .get('https://localhost:7157/flows/' + flowKey)
    .then(response => {
        //console.log(response.data.data);
        stateStore.$id = response.data.id
        stateStore.save(response.data.data)
        restoreState()
    })
    .catch(error => console.log(error));
});

</script>

<template>
    <VueFlow :id=flowKey
        :auto-connect=connector
    >
    <Panel>
        <button @click="addNode('input')">Add input node</button>
        <button @click="addNode('default')">Add processing node</button>
        <button @click="addNode('output')">Add output node</button>
        <button @click="saveState">Save state</button>
    </Panel>
    </VueFlow>
</template>

<style>
</style>