<script lang="ts" setup>
import { ref, computed } from 'vue'

const author = ref({
  name: 'John Doe',
  books: [
    'Vue 2 - Advanced Guide',
    'Vue 3 - Basic Guide',
    'Vue 4 - The Mystery'
  ]
})

// a computed ref
const publishedBooksMessage = computed(() => {
  return author.value.books.length > 0 ? 'Yes' : 'No'
})

const orderByBooks = computed(() => {
  const reverse = author.value.books.reverse()
  return reverse
})

const firstName = ref('John')
const lastName = ref('Doe')

const fullName = computed({
  // getter
  get() {
    return firstName.value + ' ' + lastName.value
  },
  // setter
  set(newValue) {
    // Note: we are using destructuring assignment syntax here.
    [firstName.value, lastName.value] = newValue.split(' ')
  }
})

const count = ref(0)

const alwaysSmall = computed({
  get(previous) {
    if (count.value <= 3) {
      return count.value
    }

    return previous
  },
  set(newValue) {
    console.log(newValue)
    count.value = newValue * 2
  }
})

const isActive = ref(true)
const error = ref(null)

const classObject = computed(() => ({
  active: isActive.value && !error.value,
  'text-danger': error.value && error.value === 'fatal'
}))

const activeColor = ref('red')
const fontSize = ref(30)

const styleObject = ref({
  color: 'green',
  fontSize: '30px'
})

const items = ref([{ message: 'Foo' }, { message: 'Bar' }])

interface TaskType {
    name: string,
    done: boolean
}
const todos = ref<TaskType[]>([
    { name: 'task-1', done: true },
    { name: 'task-2', done: false },
    { name: 'task-3', done: true }
])

function say(message, event) {
  console.log(event)
  alert(message)
}

function onScroll() {
    console.log('onScroll')
}
</script>

<template>
<p>Has published books:</p>
<span>{{ publishedBooksMessage }}</span>
<ul>
    <li v-for="book in orderByBooks">
        {{ book }}
    </li>
</ul>
<hr />
<h3>Writable Computed</h3>
<div>{{ fullName }}</div>
<div>{{ alwaysSmall }}</div>
<button @click="count++">Count</button>
<hr />
<div :class="classObject">5555</div>
<hr />
<div :style="{ color: activeColor, fontSize: fontSize + 'px' }">Binding Inline Styles</div>
<div :style="styleObject">Binding Inline Styles</div>
<div :style="{ display: ['-webkit-box', '-ms-flexbox', 'flex'] }">Multiple Values</div>
<hr />
<h3>List Rendering</h3>
<li v-for="(item, index) in items">
    {{index}} - {{ item.message }}
</li>
<span v-for="n in 10">{{ n }}</span>

<ul>
  <template v-for="item in items">
    <li>{{ item.message }}</li>
    <li class="divider" role="presentation"></li>
  </template>
</ul>

<template v-for="todo in todos" :key="todo.name">
  <li v-if="!todo.done">
    {{ todo.name }}
  </li>
</template>

<button @click="(event) => say('hello', event)">Say hello</button>
<button @click.prevent="say('bye', $event)">Say bye</button>

<div @scroll.passive="onScroll">...</div>

</template>

<style scope>
.text-danger {
    color: red;
}
</style>