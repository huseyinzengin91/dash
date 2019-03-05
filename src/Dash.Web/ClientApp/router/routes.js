import CounterExample from 'components/counter-example'
import FetchData from 'components/fetch-data'
import Dashboard from 'components/pages/dashboard'

export const routes = [
  { name: 'dashboard', path: '/', component: Dashboard, display: 'Dashboard', icon: 'home' },
  { name: 'counter', path: '/counter', component: CounterExample, display: 'Counter', icon: 'graduation-cap' },
  { name: 'fetch-data', path: '/fetch-data', component: FetchData, display: 'Fetch data', icon: 'list' }
]
