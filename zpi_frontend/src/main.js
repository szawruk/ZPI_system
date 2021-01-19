import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store/store'

import MainPage from "@/components/MainPage.vue";

const app = createApp(App)
app.use(store)
app.use(router)
app.component('MainPage', MainPage)
app.mount('#app')

