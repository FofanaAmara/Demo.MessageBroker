@echo off

cd C:\Users\Ordi\source\repos\Demo.MessageBroker

start "Souscripteur 1" Demo.PubSub.Souscripteur\bin\debug\Demo.PubSub.Souscripteur.exe  souscription1
start "Souscripteur 2" Demo.PubSub.Souscripteur\bin\debug\Demo.PubSub.Souscripteur.exe  souscription2
start "Souscripteur 3" Demo.PubSub.Souscripteur\bin\debug\Demo.PubSub.Souscripteur.exe  souscription3