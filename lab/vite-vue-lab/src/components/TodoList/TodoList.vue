<script lang="ts" setup>
import { ref, watch, watchEffect, useTemplateRef } from 'vue'

interface TodoItem {
    name: string,
    priority: number,
    done: boolean,
    subTasks: []
}

const todoItems = ref<TodoItem[]>([])
let newTodoItem = ref<TodoItem>({subTasks: []})
function addItem(){
    if(newTodoItem.value.name) {
        todoItems.value.push(newTodoItem.value)
        newTodoItem = ref<TodoItem>({subTasks: []})
    }
}

const question = ref('')
const answer = ref('Questions usually contain a question mark. ;-)')
const img = ref('')
const loading = ref(false)
// watch works directly on a ref
watch(question, async (newQuestion, oldQuestion) => {
  if (newQuestion.includes('?')) {
    loading.value = true
    answer.value = 'Thinking...'
    await fetch('https://yesno.wtf/api')
        .then(async (res) => {
            const resp = (await res.json())
            answer.value = resp.answer
            img.value = resp.image
        })
        .catch(error => answer.value = 'Error! Could not reach the API. ' + error)
        .finally(() => loading.value = false)
  }
})

const x = ref(0)
const y = ref(0)
const refData = useTemplateRef('refData')

// single ref
watch(x, (newX) => {
  console.log(`x is ${newX}`)
},
  { immediate: true })

// getter
const unwatch = watch(
  () => x.value + y.value,
  (sum) => {
    console.log(`sum of x + y is: ${sum}`)
    refData.value.value = sum
  }
)

// array of multiple sources
watch([x, () => y.value], ([newX, newY]) => {
  console.log(`x is ${newX} and y is ${newY}`)
})

const dataTodoId = ref(null)
const todoId = ref(0)
watchEffect(async (onCleanup) => {
  if(!todoId.value) return
  await fetch(
    `https://jsonplaceholder.typicode.com/todos/${todoId.value}`
  )
  .then(async response => dataTodoId.value = await response.json())
  .catch(error => console.log(error))

  await onCleanup(() => console.log('onCleanup'))
})


</script>

<template>
    <div>
        <div><input type="text" v-model.trim="newTodoItem.name" placeholder="name"></div>
        <div><input type="number" v-model="newTodoItem.priority" placeholder="priority"></div>
        <div><input type="checkbox" v-model="newTodoItem.done">Done</div>
        <input type="checkbox" id="t1" value="T1" v-model="newTodoItem.subTasks"><label for="t1">T1</label>
        <input type="checkbox" id="t2" value="T2" v-model="newTodoItem.subTasks"><label for="t2">T2</label>
        <input type="checkbox" id="t3" value="T3" v-model="newTodoItem.subTasks"><label for="t3">T3</label>
        <div><button @click="addItem">Add</button></div>
    </div>
    <hr></hr>
    <div v-for="item in todoItems">
        <div>{{ item.name }}: {{ item.priority }} : Done <input type="checkbox" :checked="item.done"> : {{ item.subTasks }}</div>
    </div>
    <hr></hr>
    x: <input type="number" v-model="x">
    y: <input type="number" v-model="y">
<hr></hr>
    <p>
    Ask a yes/no question:
    <input v-model="question" :disabled="loading" />
  </p>
  <p>{{ answer }} <img :src="img"></img></p>
  <hr></hr>
  <div>Todo ID: <input type="number" v-model="todoId"></div>
  <div>{{ dataTodoId }}</div>
  <button @click="unwatch">Unwatch</button>
  <hr></hr>
  <input type="text" ref="refData">
</template>

<style lang="css">
</style>