<script lang="ts" setup>
import { ref } from 'vue'

const msg = ref("Hello World")
const rawHtml = ref("<span style='color: red'>This should be red.</span>")
const dynamicId = ref()
const isButtonDisabled = ref(true)
const objectOfAttrs = ref({
  id: 'container',
  class: 'wrapper',
  style: 'background-color:green',
  value: 'Hello',
  type: 'text'
})
const number = ref(1)
const ok = ref("YES")
const message = ref("YES")
const id = ref("12345")
const date = ref(new Date())

function toTitleDate(date: Date): string{
    return date.toDateString()
}

function formatDate(date: Date) {
    console.log(date)
}

const seen = ref(false)
const url = ref("https://my-parinya.gitbook.io/open-practice-library/")

function alertBox(alertMessage: string){
    alert(alertMessage)
}

const redirect = ref('target')
</script>

<template>
    <h3>​​Text Interpolation</h3>
    <span>Message: {{ msg }}</span>
    <hr />
    <h3>​​Raw HTML</h3>
    <p>Using text interpolation: {{ rawHtml }}</p>
    <p>Using v-html directive: <span v-html="rawHtml"></span></p>
    <hr />
    <h3>Attribute Bindings</h3>
    <div v-bind:id="dynamicId">Hello</div>
    <div :id="dynamicId">Shorthand</div>
    <button :disabled="isButtonDisabled">Button</button>
    <input v-bind="objectOfAttrs"></input>
    <hr />
    <h3>Using JavaScript Expressions</h3>
    {{ number + 1 }}
    {{ ok ? 'YES' : 'NO' }}
    {{ message.split('').reverse().join('') }}
    <div :id="`list-${id}`"></div>
    <time :title="toTitleDate(date)" :datetime="date.toISOString()">
        {{ formatDate(date) }}
    </time>
    <p v-if="seen">Now you see me</p>
    <a :href="url" :[redirect]="'_blank'"> My open-practice-library </a>
    <a v-on:click="alertBox('Click 1')"> Click 1 </a>
    <!-- shorthand -->
    <a @click="alertBox('Click 2')"> Click 2 </a>
</template>

<style scoped>
</style>