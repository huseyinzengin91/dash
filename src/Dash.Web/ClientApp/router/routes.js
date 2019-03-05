import CounterExample from 'components/counter-example'
import FetchData from 'components/fetch-data'
import Dashboard from 'components/pages/dashboard'
import Sites from 'components/pages/sites/list'
import AddSite from 'components/pages/sites/add'

export const routes = [
  { name: 'dashboard', path: '/', component: Dashboard, display: 'Dashboard', icon: 'home' },
  { name: 'counter', path: '/counter', component: CounterExample, display: 'Counter', icon: 'graduation-cap' },
  { name: 'fetch-data', path: '/fetch-data', component: FetchData, display: 'Fetch data', icon: 'list' },
  { name: 'sites', path: '/sites', component: Sites, display: 'Sites', icon: 'list' },
  { name: 'add-site', path: '/add-site', component: AddSite, display: 'Add Site', hidden: true },
]
