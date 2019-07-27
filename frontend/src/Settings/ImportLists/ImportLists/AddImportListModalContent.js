import PropTypes from 'prop-types';
import React, { Component } from 'react';
import { kinds } from 'Helpers/Props';
import Alert from 'Components/Alert';
import Button from 'Components/Link/Button';
import FieldSet from 'Components/FieldSet';
import LoadingIndicator from 'Components/Loading/LoadingIndicator';
import ModalContent from 'Components/Modal/ModalContent';
import ModalHeader from 'Components/Modal/ModalHeader';
import ModalBody from 'Components/Modal/ModalBody';
import ModalFooter from 'Components/Modal/ModalFooter';
import AddImportListItem from './AddImportListItem';
import styles from './AddImportListModalContent.css';

class AddImportListModalContent extends Component {

  //
  // Render

  render() {
    const {
      isSchemaFetching,
      isSchemaPopulated,
      schemaError,
      spotifyLists,
      lastFmLists,
      otherLists,
      onImportListSelect,
      onModalClose
    } = this.props;

    return (
      <ModalContent onModalClose={onModalClose}>
        <ModalHeader>
          Add List
        </ModalHeader>

        <ModalBody>
          {
            isSchemaFetching &&
              <LoadingIndicator />
          }

          {
            !isSchemaFetching && !!schemaError &&
              <div>Unable to add a new list, please try again.</div>
          }

          {
            isSchemaPopulated && !schemaError &&
              <div>

                <Alert kind={kinds.INFO}>
                  <div>Lidarr supports multiple lists for importing Albums and Artists into the database.</div>
                  <div>For more information on the individual lists, click on the info buttons.</div>
                </Alert>

                <FieldSet legend="Spotify Lists">
                  <div className={styles.lists}>
                    {
                      spotifyLists.map((list) => {
                        return (
                          <AddImportListItem
                            key={list.implementation}
                            implementation={list.implementation}
                            {...list}
                            onImportListSelect={onImportListSelect}
                          />
                        );
                      })
                    }
                  </div>
                </FieldSet>

                <FieldSet legend="Last.fm Lists">
                  <div className={styles.lists}>
                    {
                      lastFmLists.map((list) => {
                        return (
                          <AddImportListItem
                            key={list.implementation}
                            implementation={list.implementation}
                            {...list}
                            onImportListSelect={onImportListSelect}
                          />
                        );
                      })
                    }
                  </div>
                </FieldSet>

                <FieldSet legend="Other">
                  <div className={styles.lists}>
                    {
                      otherLists.map((list) => {
                        return (
                          <AddImportListItem
                            key={list.implementation}
                            implementation={list.implementation}
                            {...list}
                            onImportListSelect={onImportListSelect}
                          />
                        );
                      })
                    }
                  </div>
                </FieldSet>
              </div>
          }
        </ModalBody>
        <ModalFooter>
          <Button
            onPress={onModalClose}
          >
            Close
          </Button>
        </ModalFooter>
      </ModalContent>
    );
  }
}

AddImportListModalContent.propTypes = {
  isSchemaFetching: PropTypes.bool.isRequired,
  isSchemaPopulated: PropTypes.bool.isRequired,
  schemaError: PropTypes.object,
  spotifyLists: PropTypes.arrayOf(PropTypes.object).isRequired,
  lastFmLists: PropTypes.arrayOf(PropTypes.object).isRequired,
  otherLists: PropTypes.arrayOf(PropTypes.object).isRequired,
  onImportListSelect: PropTypes.func.isRequired,
  onModalClose: PropTypes.func.isRequired
};

export default AddImportListModalContent;
