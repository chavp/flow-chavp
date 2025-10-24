<script lang="ts" setup>
import { ref, onMounted } from 'vue'

import type { Node, Edge, FlowExportObject } from '@vue-flow/core'
import { VueFlow, useVueFlow } from '@vue-flow/core'

import { uuid } from 'vue-uuid'
import { defineStore } from 'pinia'
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import axios from 'axios';

import MachineNode from './MachineNode.vue'

const baseUrl = "https://localhost:7157"
// storange
const flowId = '7225091a-543b-4113-8b55-ef4e3ca5c7af'
const initNodes = ref<Node[]>([
  {
    id: uuid.v4(),
    type: 'machine',
    position: { x: 100, y: 100 },
    data: {
        name: 'Local',
        label: 'Local',
        totalSeconds: 0
    }
  },
])

interface MachineInfo {
    name: string,
    totalProcessTimeSeconds: number,
    currentTime: Date,
}
const {
    updateNodeData,
    toObject,
    fromObject,

    setNodes,
    setEdges,
    setViewport,

} = useVueFlow(flowId)

async function restoreState(flowData: FlowExportObject) {
  if (flowData) {
    fromObject(flowData)
  }
}

const useStateStore = defineStore(flowId, {
  state: () => (
      { 
        data: {
          nodes: [] as Node[],
          edges: [] as Edge[],
          viewport: { x: 0, y: 0, zoom: 1 }
        } as FlowExportObject
      }
    )
})
const stateStore = useStateStore()

onMounted(async () => {
    await loadFlow(baseUrl, flowId)
        .then(() => connectSignalR())
        .catch(error => console.log(error))
});

async function loadFlow(baseUrl: string, folwId: string) {
  return axios
    .get(`${baseUrl}/flows/${folwId}`)
    .then(response => {
        //console.log(response.data.data);
        stateStore.$id = response.data.id
        stateStore.data = response.data.data
        restoreState(response.data.data)
    })
    .catch(error => {
        if (error.response && error.response.status === 404) {
          console.error('Resource not found (404 Error):', error.response.data);
          // Implement specific 404 handling
          stateStore.data = toObject()

          return axios
            .put(`${baseUrl}/flows/${folwId}`, stateStore)
            .then(response => {
                //console.log(response.data.data);
                stateStore.$id = response.data.id
                stateStore.data = response.data.data
                restoreState(response.data.data)
            })
        } else {
          console.error('An unexpected error occurred:', error.message);
          // Handle other types of errors
          throw error;
        }
    })
}

async function connectSignalR(){
    let timeInSeconds = 0;
    const connection = new HubConnectionBuilder()
            .withUrl(`${baseUrl}/hubs/clock`) // Replace with your SignalR hub URL
            .configureLogging(LogLevel.Information)
            .withAutomaticReconnect([3000, 5000, 8000, 13000])
            .build();

    connection.on("showtime", (nodeId:string, machineInfo: MachineInfo) => {
        //messages.value.push(`${user}: ${message}`);
        //console.log(machineInfo)
        timeInSeconds++
        updateNodeData(nodeId, { ...machineInfo  })
    })
    
    return connection.start()
        .then(resp => {
            console.log("SignalR Connected.");
        })
}

</script>

<template>
    <VueFlow 
        :id=flowId
        >
        <template #node-machine=props>
            <MachineNode :id=props.id  :data=props.data :base-url=baseUrl />
        </template>
    </VueFlow>
</template>

<style lang="css">
</style>