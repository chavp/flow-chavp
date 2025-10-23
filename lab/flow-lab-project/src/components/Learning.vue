<script setup lang="ts">
import { ref } from 'vue'
import { markRaw } from 'vue'
import type { Node, Edge } from '@vue-flow/core'  
import { isErrorOfType, ErrorCode, VueFlow, Panel, useVueFlow, Position, ConnectionMode } from '@vue-flow/core'
import { type HandleConnection, useNodeConnections } from '@vue-flow/core'
//import { type HandleConnection, useHandleConnections, useNodesData } from '@vue-flow/core'
import { Background } from '@vue-flow/background'
import { MiniMap } from '@vue-flow/minimap'
import { Controls } from '@vue-flow/controls'

// these components are only shown as examples of how to use a custom node or edge
// you can find many examples of how to create these custom components in the examples page of the docs
import SpecialNode from './SpecialNode.vue'
import SpecialEdge from './SpecialEdge.vue'
import CustomNode from './CustomNode.vue'
import CustomEdge from './CustomEdge.vue'
import Sidebar from './Sidebar.vue'

import { initialEdges, initialNodes } from '../data/initial-elements.js'

type CustomEdgeTypes = 'custom' | 'special'

type CustomEdge = Edge<CustomData, any, CustomEdgeTypes>

const edgeTypes = {
  custom: CustomEdge,
}

const nodeTypes = {
  custom: CustomNode,
}

const flowId = ref('my-flow')
const { 
    onInit, 
    findNode, 
    fitView, 
    snapToGrid,
    addNodes,
    removeNodes,
    addEdges,
    removeEdges,

    // useVueFlow provides access to the event handlers
    onNodeDragStart, 
    onNodeDrag,
    onNodeDragStop, 
    onNodeClick, 
    onNodeDoubleClick, 
    onNodeContextMenu, 
    onNodeMouseEnter, 
    onNodeMouseLeave, 
    onNodeMouseMove,

    onEdgeClick,
    onEdgeDoubleClick,
    onEdgeContextMenu,
    onEdgeMouseEnter,
    onEdgeMouseLeave,
    onEdgeMouseMove,
    onEdgeUpdateStart,
    onEdgeUpdate,
    onEdgeUpdateEnd,
    
    onNodesChange, 
    onEdgesChange,

    applyNodeChanges,
    applyEdgeChanges,

    updateNode,
    findEdge,

    onPaneReady,
    onError,
} = useVueFlow(flowId.value)

onError(handleError)

function handleError(error: VueFlowError) {
  console.log(error)
  if (isErrorOfType(error, ErrorCode.NODE_INVALID)) {
    const [nodeId] = error.args
    // handle the error
  }
}

snapToGrid.value = true

// any event that is emitted from the `<VueFlow />` component can be listened to using the `onEventName` method
onInit((instance) => {
  // `instance` is the same type as the return of `useVueFlow` (VueFlowStore)
  
  //fitView()
  
  const node = findNode('1')
  
  if (node) {
    node.position = { x: 100, y: 0 }
  }
})

// get all connections where this node is the target (incoming connections)
const targetConnections = useNodeConnections({
  // type is required
  handleType: 'target',
})

// get all connections where this node is the source (outgoing connections)
const sourceConnections = useNodeConnections({
  handleType: 'source',
})

const handleConnections = useNodeConnections({
  handleId: 'handle-1', // you can explicitly pass a handle id if you want to get connections of a specific handle
})

const connections = useNodeConnections({
  nodeId: '6', // you can explicitly pass a node id, otherwise it's used from the `NodeId  injection
  handleType: 'target',
  onConnect: (connections: HandleConnection[]) => {
    // do something with the connections
    console.log("onConnect")
  },
  onDisconnect: (connections: HandleConnection[]) => {
    // do something with the connections
     console.log("onDisconnect")
  },
})

const edgesUpdatable = ref(false)

// these are our nodes
const nodes = ref<Node[]>([
  // an input node, specified by using `type: 'input'`
  {
    id: '1',
    type: 'input',
    position: { x: 250, y: 5 },
    // all nodes can have a data object containing any data you want to pass to the node
    // a label can property can be used for default nodes
    data: { label: 'Node 1' },
    extent: [[-250, -250], [250, 250]],
    
  },

  // default node, you can omit `type: 'default'` as it's the fallback type
  {
    id: '2',
    position: { x: 100, y: 100 },
    data: { label: 'Node 2' },
  },

  // An output node, specified by using `type: 'output'`
  {
    id: '3',
    type: 'output',
    position: { x: 400, y: 200 },
    data: { label: 'Node 3' },
  },

  // this is a custom node
  // we set it by using a custom type name we choose, in this example `special`
  // the name can be freely chosen, there are no restrictions as long as it's a string
  {
    id: '4',
    type: 'special', // <-- this is the custom node type name
    position: { x: 200, y: 300 },
    data: {
      label: 'Node 4',
      hello: 'world',
    },
  },

  {
    id: '5',
    type: 'default', // <-- this is the custom node type name
    position: { x: 300, y: 400 },
    targetPosition: Position.Left,
    sourcePosition: Position.Right,
    data: {
      label: 'Node 5',
      hello: 'world',
    },
  },

  {
    id: '6',
    type: 'custom', // <-- this is the custom node type name
    position: { x: 400, y: 500 },
    data: {
      label: 'Node 6',
    },
  },
])

// these are our edges
const edges = ref<Edge[]>([
  // default bezier edge
  // consists of an edge id, source node id and target node id
  {
    id: 'e1->2',
    source: '1',
    target: '2'
  },

  // set `animated: true` to create an animated edge path
  {
    id: 'e2->3',
    source: '2',
    target: '3',
    animated: true,
  },

  // a custom edge, specified by using a custom type name
  // we choose `type: 'special'` for this example
  {
    id: 'e3->4',
    type: 'special',
    source: '3',
    target: '4',

    // all edges can have a data object containing any data you want to pass to the edge
    data: {
      hello: 'world',
    }
  },

  // set `animated: true` to create an animated edge path
  {
    id: 'e4->5',
    type: 'custom',
    source: '4',
    target: '5',
    animated: true,
    updatable: true, 
  },
])

function addNode() {
  const id = Date.now().toString()
  
  addNodes({
    id,
    position: { x: 150, y: 50 },
    data: { label: `Node ${id}`, },
  })
}

function generateRandomNode() {
  return {
    id: Math.random().toString(),
    position: { x: Math.random() * 500, y: Math.random() * 500 },
    label: 'Random Node',
  }
}

function onAddNode() {
  // add a single node to the graph
  addNodes(generateRandomNode())
}

function onAddNodes() {
  // add multiple nodes to the graph
  addNodes(Array.from({ length: 10 }, generateRandomNode))
}

function onRemoveNode(id) {
  removeNodes(id)
}

function onUpdateNode(id) {
  updateNode(id, { data: { label: 'Node Update' } })
}

function onConnect(sourceId, targetId) {
  addEdges([
    {
      id: sourceId + '->' + targetId,
      type: 'custom',
      source: sourceId,
      target: targetId,

      // if a node has multiple handles of the same type,
      // you should specify which handle to use by id
      sourceHandle: null,
      targetHandle: null,
    }
  ])

}
function onUnconnect(edgeId) {
  removeEdges(edgeId)  
}


function onConnecting(edgeId) {
  const edge = findEdge(edgeId)
  edge.animated = true
}

export interface CustomData {
  hello: string
}

export interface CustomEvents {
  onCustomEvent: (event: MouseEvent) => void
}

type CustomNodeTypes = 'custom' | 'special'

type CustomNode = Node<CustomData, CustomEvents, CustomNodeTypes>

// bind listeners to the event handlers
onNodeDragStart((event) => {
  console.log('Node drag started', event)
})

onNodeDrag((event) => {
  console.log('Node dragged', event)
})

onNodeDragStop((event) => {
  console.log('Node drag stopped', event)
})
onNodeClick(({event, node}) => {
  console.log('node clicked', node, event)
})


// bind listeners to the event handlers
onEdgeClick(({event, edge}) => {
  console.log('edge clicked', edge, event)
})

onEdgeDoubleClick(({event, edge}) => {
  console.log('edge double clicked', edge, event)
})

onEdgeContextMenu(({event, edge}) => {
  console.log('edge context menu', edge, event)
})

onNodesChange((changes) => {
  // changes are arrays of type `NodeChange`
  //console.log(changes)
  applyNodeChanges(changes)
})

onEdgesChange((changes) => {
  // changes are arrays of type `EdgeChange`
  console.log(changes)
  applyEdgeChanges(changes)
})
  
const onChange = (changes) => {
  // changes are arrays of type `NodeChange` or `EdgeChange`
  console.log('onChange')

  // apply changes manually
  //applyNodeChanges(changes)
}

const connector = (params) => {

  if (params.source === params.target) {
    return false
  }
  
  return {
    id: `edge-${params.source}-${params.target}`,
    source: params.source,
    target: params.target,
    label: `Edge ${params.source}-${params.target}`,
    animated: false,
  }
}

// event handler
onPaneReady((instance) => 
{
  console.log("onPaneReady")

  //instance.project({ x: 500, y: 500 })
  //instance.fitView({padding: 0.2})
  //instance.setViewport({ x: 100, y: 100, zoom: 1.5 })
})

</script>

<template>
  <div>
    <Sidebar :flow-id=flowId />
    <div class="wrapper">
        <div class="flow-editor">
          <div class="flow-canvas">
            <VueFlow 
              :id=flowId
              :nodes="initialNodes" 
              :edges="initialEdges"
              :default-viewport="{ zoom: 1.2 }"
              :min-zoom="0.2"
              :max-zoom="4"
              :connection-mode="ConnectionMode.Strict"
              @nodes-change="onChange" 
              @edges-change="onChange"
              :apply-default="false"
              :edge-types="edgeTypes"
              :node-types="nodeTypes"
              :fit-view-on-init="true"
              :zoom-on-double-click="false"
              :edges-updatable="edgesUpdatable"
              :auto-connect="connector"
            >
              <!-- bind your custom node type to a component by using slots, slot names are always `node-<type>` -->
              <template #node-special="specialNodeProps">
                <SpecialNode v-bind="specialNodeProps" />
              </template>

              <template #node-custom="customNodeProps">
                <CustomNode v-bind="customNodeProps" />
              </template>

              <!-- bind your custom edge type to a component by using slots, slot names are always `edge-<type>` -->
              <template #edge-special="specialEdgeProps">
                <SpecialEdge v-bind="specialEdgeProps" />
              </template>

              <template #edge-custom="customEdgeProps">
                <CustomEdge v-bind="customEdgeProps" />
              </template>
              <Panel>
                <button type="button" @click="addNode">Add a node</button>
                <button type="button" @click="onAddNode">Add a node</button>
                <button type="button" @click="onAddNodes">Add multiple nodes</button>
                <button type="button" @click="onRemoveNode('1')">Remove Node 1</button>
                <button type="button" @click="onUpdateNode('1')">Undate Node 1</button>
                <button type="button" @click="onConnect('1', '6')">Connect Node 1 -> 6</button>
                <button type="button" @click="onUnconnect('1->6')">Unconnect Node 1 -> 6</button>
                <button type="button" @click="onConnecting('1->6')">Connecting Node 1 -> 6</button>
              </Panel>
              
              <Background />
              <MiniMap pannable zoomable />
              <Controls />
            </VueFlow>
          </div>
        </div>
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

:root {
    --vf-node-bg: #fff;
    --vf-node-text: #222;
    --vf-connection-path: #b1b1b7;
    --vf-handle: #555;
}


.wrapper {
  width: 100px;
}

.flow-editor {
  width: 100vw;
  height: 100vh;
  position: fixed;
  top: 0;
  left: 200px;
}

/* Dark theme support */
@media (prefers-color-scheme: dark) {
  .flow-canvas {
    width: 90%;
    height: 100%;
    background: #0f1419;
    background-image: 
      radial-gradient(circle, #374151 1px, transparent 1px);
    background-size: 20px 20px;
  }
}
</style>