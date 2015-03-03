using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNarrareEditPrototype
{
    class StoryWorld
    {
        /* Variables */
        public string name;
        public List<Entity> entities;
        public List<Attribute> attributes;
        public List<Relationship> relationships;

        /* Functions */
        public StoryWorld(string _name)
        {
            name = _name;
            entities = new List<Entity>();
            attributes = new List<Attribute>();
            relationships = new List<Relationship>();
        }

        public void setName(string _name)
        {
            name = _name;
        }

        /* Entity Functions */
        public void addNewEntity(string name)
        {
            entities.Add(new Entity(name));
        }

        public void addEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public void removeEntity(int key)
        {
            entities.RemoveAt(key);
        }

        public void changeEntityName(int key, string newName)
        {
            entities[key].setName(newName);
        }

        /* Attribute Functions */
        public void addNewAttribute(string name)
        {
            attributes.Add(new Attribute(name));
        }

        public void addAttribute(Attribute attribute)
        {
            attributes.Add(attribute);
        }

        public void removeAttribute(int key)
        {
            attributes.RemoveAt(key);
        }

        public void changeAttributeName(int key, string newName)
        {
            attributes[key].setName(newName);
        }

        /* Relationship Functions */
        public void addNewRelationship(string name)
        {
            relationships.Add(new Relationship(name));
        }

        public void addRelationship(Relationship relationship)
        {
            relationships.Add(relationship);
        }

        public void removeRelationship(int key)
        {
            relationships.RemoveAt(key);
        }

        public void changeRelationshipName(int key, string newName)
        {
            relationships[key].setName(newName);
        }

        /* Misc Functions */
        public StoryWorld copy(StoryWorld storyWorld)
        {
            storyWorld.setName(name);
            
            Entity tempA;

            for (int i = 0; i < entities.Count; i++)
            {
                tempA = new Entity("");
                entities[i].copy(tempA);
                storyWorld.addEntity(tempA);
            }

            Attribute tempB;

            for (int i = 0; i < attributes.Count; i++)
            {
                tempB = new Attribute("");
                attributes[i].copy(tempB);
                storyWorld.addAttribute(tempB);
            }

            Relationship tempC;

            for (int i = 0; i < relationships.Count; i++)
            {
                tempC = new Relationship("");
                relationships[i].copy(tempC);
                storyWorld.addRelationship(tempC);
            }
          
            return storyWorld;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
